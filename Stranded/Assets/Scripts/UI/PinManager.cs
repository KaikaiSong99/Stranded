using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PinManager : MonoBehaviour
{

    public TextMeshProUGUI JoinRoomText;
    public Button Button0;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public Button Button5;
    public Button Button6;
    public Button Button7;
    public Button Button8;
    public Button Button9;
    public Button ClearButton;
    public Button BackButton;    

    public static event Action<string> onPinSent;

    private const string prefix = "JOIN ROOM: ";
    private string pincode = "";

    void Start() {
        Button0.onClick.AddListener(() => AddDigit("0"));
        Button1.onClick.AddListener(() => AddDigit("1"));
        Button2.onClick.AddListener(() => AddDigit("2"));
        Button3.onClick.AddListener(() => AddDigit("3"));
        Button4.onClick.AddListener(() => AddDigit("4"));
        Button5.onClick.AddListener(() => AddDigit("5"));
        Button6.onClick.AddListener(() => AddDigit("6"));
        Button7.onClick.AddListener(() => AddDigit("7"));
        Button8.onClick.AddListener(() => AddDigit("8"));
        Button9.onClick.AddListener(() => AddDigit("9"));
        ClearButton.onClick.AddListener(() => {
            pincode = "";
            JoinRoomText.text = prefix;
        } );
        BackButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
            pincode = "";
            JoinRoomText.text = prefix;
        } );

        JoinRoomText.text = prefix;
    }

    private void AddDigit(string digit)
    {
        if (pincode.Length < 4) 
        {
            pincode += digit;
            JoinRoomText.text = prefix + pincode;
        }
    }

    public void SendPin()
    {
        onPinSent?.Invoke(pincode);
        pincode = "";
        JoinRoomText.text = prefix;
    }

}
