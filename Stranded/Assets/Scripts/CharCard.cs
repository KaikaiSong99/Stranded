using System.Collections;
using System.Collections.Generic;
using Model;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharCard : MonoBehaviour, IPointerDownHandler
{
   
    public Text characterName;
    public Image portrait;
   
    public Button infoButton;
    private Character _character;
    public CanvasGroup infoUI;

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
        Button btn = infoButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        infoUI.gameObject.SetActive(true);
        var cardInfo = infoUI.GetComponent<CharCardDisplayInfo>();
        if (cardInfo != null)
        {
            cardInfo.SetCharacterInfo(character);
        }
    }

    public void CloseInfo()
    {
        infoUI.gameObject.SetActive(false);
    }
  
    public void OnPointerDown(PointerEventData data)
    {
        
       charactersView.SetActive(false);
       
       characterManager.roundManager.AssignCharacterToJob(character, jobCard.job);
       characterManager.roundManager.jobManager.RefreshJobCards();


    }

}
