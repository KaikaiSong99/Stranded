using System.Collections;
using System.Collections.Generic;
using Model;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharCard : MonoBehaviour
{
    public Text characterName;
    public Image portrait;
   
    public Button infoButton;
    private Character _character;
    public GameObject infoUI;

    public CharacterManager characterManager;
    public GameObject charactersView;
    
    public JobCard jobCard;
    public Character character
    {
        get { return _character;}
        set { 
            _character = value; characterName.text = value.name;
            portrait.sprite = value.portrait;
            infoButton.GetComponentInChildren<Text>().text = "?";

        }
    }
    public void Start () {
        infoButton.onClick.AddListener(OnInfoButtonClick);
    }

    private void OnInfoButtonClick()
    {
        infoUI.SetActive(true);
        var cardInfo = infoUI.GetComponent<CharCardDisplayInfo>();
        if (cardInfo != null)
        {
            cardInfo.SetCharacterInfo(character);
        }
    }

    public void CloseInfo()
    {
        infoUI.SetActive(false);
    }
  
    public void OnCharCardClick()
    { 
        charactersView.SetActive(false);

        characterManager.roundManager.AssignCharacterToJob(character, jobCard.job);
        characterManager.roundManager.jobManager.RefreshJobCards();
    }
}
