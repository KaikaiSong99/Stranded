using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JobManager : MonoBehaviour
{
    //public CanvasGroup assignment;
    private JobCard _currentJobCard;
    private Job _currentJob;
    
    public GameObject jobCardPrefab;
    public List<Transform> containers;
    private List<GameObject> objects = new List<GameObject>();
    public CanvasGroup charactersUI;
    public CharacterManager characterManager;
    public RoundManager roundManager;
    

    public void Start()
    {
    }

    public void CreateCards(Dilemma dilemma) //create Job cards
    {
        ClearCards();
        for (int i=0; i<dilemma.jobs.Count; i++)
        {
            Job job = dilemma.jobs[i];
            GameObject jobCard = Instantiate(jobCardPrefab, containers[i].transform);
            objects.Add(jobCard);
            jobCard.transform.parent = containers[i];
            JobCard jCard = jobCard.GetComponent<JobCard>();
            jCard.characterManager = characterManager;
            jCard.charactersUI = charactersUI;
            jCard.dilemma = dilemma;
            
            jCard.roundManager = roundManager;
            jCard.job = job;
        }
    }

    public void RefreshJobCards()
    {
        
        foreach (GameObject jobCard in objects)
        {
            JobCard jCard = jobCard.GetComponent<JobCard>();
            jCard.charactersUI = charactersUI;
            Character characterTemp;
            if (roundManager.round.PickedCharacters.TryGetValue(jCard.job, out characterTemp))
            {
                jCard.characterName.text = characterTemp.name;
                jCard.portrait.sprite = characterTemp.portrait;
            }
			else
			{
				jCard.characterName.text = "????";
                jCard.portrait.sprite = null;
			}
       
        }
        
    }

    public void ClearCards()
    {
        foreach (var card in objects)
        {
            Destroy(card);
        }
    }
}