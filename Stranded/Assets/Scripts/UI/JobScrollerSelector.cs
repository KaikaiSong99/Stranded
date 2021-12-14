using System.Collections.Generic;
using System.Linq;
using System;
using Model;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UI
{
  public class JobScrollerSelector : MonoBehaviour
  {
    public RectTransform selectedJobFrame;

    public ScrollRect scrollRect;

    public RectTransform scrollContent;

    public float lockSpeedMultiplier = 200;

    public JobItemCreator jobItemCreator;
    
    public DataDisplay dataDisplay;

    private class JobItemInfo
    {
      public readonly Job Job;
      public readonly RectTransform ContainerTransform;
      public readonly RectTransform ItemTransform;

      public JobItemInfo(Job job, RectTransform itemTransform, RectTransform containerTransform)
      {
        Job = job;
        ContainerTransform = containerTransform;
        ItemTransform = itemTransform;
      }
    }

    private IList<JobItemInfo> _jobItemInfos;

    private JobItemInfo _selectedJobInfo;

    public Job SelectedJob => _selectedJobInfo.Job;

    public void OnDisplay(Job job)
    {
      jobItemCreator.OnDisplay();
      
      _jobItemInfos = scrollContent.GetComponentsInChildren<JobItem>()
        .Select(jobItem => new JobItemInfo(jobItem.job, jobItem.GetComponent<RectTransform>(), 
          jobItem.transform.parent.GetComponent<RectTransform>()))
        .ToList();
      
      OnScroll();
      
      // TODO Snap to job
      Debug.Log($"Snap to job {job.name}");
    }

    public void OnScroll()
    {
      if (_jobItemInfos != null) // OnScroll depends on there being info on job items
      {
        var selectedJobFramePos = selectedJobFrame.position;

        Assert.IsTrue(_jobItemInfos.Count > 0);
        _selectedJobInfo = _jobItemInfos.Aggregate((job1Info, job2Info) =>
        {
          var job1DistanceToFrame = (job1Info.ContainerTransform.position - selectedJobFramePos).magnitude;
          var job2DistanceToFrame = (job2Info.ContainerTransform.position - selectedJobFramePos).magnitude;

          return job1DistanceToFrame < job2DistanceToFrame ? job1Info : job2Info;
        });
      
        dataDisplay.SetJobInfo(SelectedJob);
        
      }
    }

    public void Update()
    {
      if (_selectedJobInfo != null)
      {
        scrollRect.velocity = lockSpeedMultiplier * new Vector2(0,
          (selectedJobFrame.position - _selectedJobInfo.ContainerTransform.position).y);
      }
    }
  }
}