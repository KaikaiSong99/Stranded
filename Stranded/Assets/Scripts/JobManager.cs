using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.EventSystems;

public class JobManager : MonoBehaviour
{
    //public CanvasGroup assignment;
    private JobCard _currentJobCard;
    private Job _currentJob;
    public GameObject jobCardPrefab;
    public List<Transform> containers;
    private List<GameObject> objects = new List<GameObject>();
    
    public void Start()
    {
    }

    public void CreateCards(List<Job> jobs)
    {
        ClearCards();
        for (int i=0; i<jobs.Count; i++)
        {
            Job job = jobs[i];
            GameObject jobCard = Instantiate(jobCardPrefab, containers[i].transform);
            objects.Add(jobCard);
            jobCard.transform.parent = containers[i];
            JobCard jCard = jobCard.GetComponent<JobCard>();
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