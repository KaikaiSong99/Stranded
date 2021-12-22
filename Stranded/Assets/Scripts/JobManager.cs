using System.Collections.Generic;
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
            jCard.charactersUI = charactersUI;
            jCard.dilemma = dilemma;
            jCard.characterManager = characterManager;
            Debug.Log(dilemma);
            jCard.job = job;
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