using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using Model;

public class JobCard : MonoBehaviour, IPointerDownHandler
{
    
    public Text jobName;
    public Text characterName;
    public Image Portrait;
    public Image icon;
    private Job _job;
    public CanvasGroup charactersUI;
    public CharacterManager characterManager;
    public Dilemma dilemma;
    

    public Job job
    {
        get { return _job;}
        set { _job = value; jobName.text = value.name;
            icon.sprite = value.jobIcon;
        }
    }
    
    // TODO: show character overview
    public void OnPointerDown(PointerEventData data)
    {
        ShowCharacters();
    }
    public void ShowCharacters()
    {
       
        charactersUI.gameObject.SetActive(true);

        characterManager.icon.sprite = icon.sprite;
        characterManager.jobName.text = jobName.text;
        characterManager.CreateCards(dilemma);
        
    }
    
}