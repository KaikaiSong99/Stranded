using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;
using Model;


[Serializable]
public struct JSONInformation
{
    public string flag;
    public List<JSONStrandedData> data;
}

[Serializable]
public struct JSONStrandedData
{
    public string dilemma;
    public string task;
    public string character;
    public string ideal;
};

public class NetworkManager : MonoBehaviour
{
    public static PubNub pubnub;
    private string pubnubUuid = "";
    private string currentChannel = "init_channel";

    public static event Action onStartReceived;

    // Start is called before the first frame update
    void Start()
    {
        InitPubnub();
        pubnub.SubscribeCallback += SubscribeCallbackHandler;
        TitleManager.onPinConfirmed += SubscribeToChannel;
        RoundManager.onRoundEnd += SendRoundData;
        GameManager.onGameEnd += UnsubscribeAll;
    }

    private void InitPubnub() 
    {
        pubnubUuid = PlayerPrefs.GetString("PUBNUB_UUID", Guid.NewGuid().ToString());

        Debug.Log(pubnubUuid);

        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.PublishKey = "pub-c-8fbea598-c1c7-407c-8810-3e1ffd745e44";
        pnConfiguration.SubscribeKey = "sub-c-e53297aa-789e-11ec-add2-a260b15b99c5";
        pnConfiguration.LogVerbosity = PNLogVerbosity.BODY;
        pnConfiguration.UUID = pubnubUuid;
        pubnub = new PubNub(pnConfiguration);

        PlayerPrefs.SetString("PUBNUB_UUID", pubnubUuid);
    }

    private void SubscribeCallbackHandler(object sender, EventArgs e)
    {
        SubscribeEventEventArgs message = e as SubscribeEventEventArgs;
        
        if (message.Status != null) {
            switch (message.Status.Category) {
                case PNStatusCategory.PNUnexpectedDisconnectCategory:
                case PNStatusCategory.PNTimeoutCategory:
                    pubnub.Reconnect();
                    break;
                case PNStatusCategory.PNConnectedCategory:
                    break;
                default:
                    break;
            }
        }

        if (message.MessageResult != null)
        {
            string messageToString = pubnub.JsonLibrary.SerializeToJsonString(message.MessageResult.Payload);
            
            if (messageToString.Contains("flag"))
            {
                JSONInformation info = JsonUtility.FromJson<JSONInformation>(messageToString);
                Debug.Log(info);
                switch(info.flag)
                {
                    case "START_GAME":
                        onStartReceived?.Invoke();
                        break;
                    default:
                        Debug.LogWarning($"Unknown flag received: {info.flag}");
                        break;
                }
            }
            
        }
        
    }

    public void SubscribeToChannel(string channel)
    {
        pubnub.UnsubscribeAll()
            .Async((result, status) => {
                if(status.Error){
                    Debug.Log (string.Format("UnsubscribeAll Error: {0} {1} {2}", status.StatusCode, status.ErrorData, status.Category));
                } else {
                    currentChannel = channel;
                    pubnub.Subscribe()
                    .Channels(new List<string>() {
                        channel
                    })
                    .Execute();
                }
            });
    }

    public void SendRoundData(Round round) 
    {
        var info = new JSONInformation();
        info.flag = "SEND_GAME_DATA";
        info.data = new List<JSONStrandedData>();
        
        foreach (var kv in round.PickedCharacters)
        {
            var jsonStranded = new JSONStrandedData();
            jsonStranded.dilemma = round.Dilemma.title;
            jsonStranded.task = kv.Key.jobTitle;
            jsonStranded.character = kv.Value.firstName;
            jsonStranded.ideal = kv.Key.idealCharacter.firstName;
            info.data.Add(jsonStranded);
        }



        pubnub.Publish()
            .Channel(currentChannel)
            .Message(info)
            .Async((result, status) => {
                if (!status.Error) {
                    Debug.Log(string.Format("DateTime {0}, In Publish Example, Timetoken: {1}", DateTime.UtcNow , result.Timetoken));
                } else {
                    Debug.Log(status.Error);
                    Debug.Log(status.ErrorData.Info);
                }
            });
    }

    public void UnsubscribeAll()
    {
        pubnub.UnsubscribeAll()
        .Async((result, status) => {
            if(status.Error){
                Debug.Log (string.Format("UnsubscribeAll Error: {0} {1} {2}", status.StatusCode, status.ErrorData, status.Category));
            } else {
                Debug.Log (string.Format("DateTime {0}, In UnsubscribeAll, result: {1}", DateTime.UtcNow, result.Message));
            }
        });;
    }

    void OnDestroy()
    {
        RoundManager.onRoundEnd -= SendRoundData;
        pubnub.SubscribeCallback -= SubscribeCallbackHandler;
        TitleManager.onPinConfirmed -= SubscribeToChannel;
    }

}
