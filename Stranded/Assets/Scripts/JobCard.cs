using UnityEngine;
using UnityEngine.UI;
using Model;

public class JobCard : MonoBehaviour
{
    public Text jobName;
    public Text characterName;
    public Text jobDescription;
    public Image portrait;
    private Job _job;
    
    [HideInInspector]
    public Image icon;
    
    [HideInInspector]
    public GameObject characterOverview;
    
    [HideInInspector]
    public CharacterManager characterManager;
    
    [HideInInspector]
    public Dilemma dilemma;
    
    [HideInInspector]
    public RoundManager roundManager;


    public void Start()
    {
        portrait.sprite = null;
    }
    

    public Job job
    {
        get { return _job;}
        set { _job = value; 
        
            jobName.text = value.name; 
            jobDescription.text = value.description;
        }
    }
    
    public void OnJobCardClick()
    {
        ShowCharacters();
    }
    
    private void ShowCharacters()
    {
        characterOverview.gameObject.SetActive(true);
        characterManager.jobCard = this;
        characterManager.jobName.text = jobName.text;
        
        characterManager.CreateCards(dilemma);
        characterManager.roundManager = roundManager;
    }
}