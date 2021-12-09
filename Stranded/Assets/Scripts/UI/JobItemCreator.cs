using UnityEngine;

namespace UI
{
  public class JobItemCreator : MonoBehaviour
  {
    public GameManager gameManager;

    public GameObject jobItemPrefab;

    public int repeats = 10;
    
    private void Start()
    {
      for (var i = 0; i < repeats; i++)
      {
        foreach (var job in gameManager.roundManager.jobs)
        {
          var jobItem = Instantiate(jobItemPrefab, transform);
          jobItem.GetComponentInChildren<JobItem>().job = job;
          jobItem.name += $" {job.name}";
        }
      }
    }
  }
}