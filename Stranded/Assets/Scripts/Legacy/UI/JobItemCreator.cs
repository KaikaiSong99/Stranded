using UnityEngine;

namespace UI
{
  public class JobItemCreator : MonoBehaviour
  {
    public _GameManager gameManager;

    public GameObject jobItemPrefab;

    public int repeats = 10;

    private bool _initialized;
    
    public void OnDisplay()
    {
      if (!_initialized)
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

        _initialized = true;
      }
    }
  }
}