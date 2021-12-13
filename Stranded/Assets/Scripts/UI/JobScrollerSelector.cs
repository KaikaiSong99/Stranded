using System.Collections.Generic;
using System.Linq;
using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class JobScrollerSelector : MonoBehaviour
  {
    public RectTransform selectedJobFrame;

    public ScrollRect scrollRect;

    public RectTransform scrollContent;

    public float lockSpeedMultiplier = 200;

    private IList<RectTransform> _jobItemTransforms;

    public RectTransform SelectedJobItem { get; private set; }

    public Job SelectedJob => SelectedJobItem.GetComponent<JobItem>().job;

    private void Start()
    {
        _jobItemTransforms = scrollContent.GetComponentsInChildren<JobItem>()
        .Select(jobItem => jobItem.GetComponent<RectTransform>())
        .ToList();

        OnScroll();
    }


    public void Reset()
    {
        SelectedJobItem = _jobItemTransforms[0];
    }

    public void OnScroll()
    {

        _jobItemTransforms = scrollContent.GetComponentsInChildren<JobItem>()
        .Select(jobItem => jobItem.GetComponent<RectTransform>())
        .ToList();


      var selectedJobFramePos = selectedJobFrame.position;
      
      SelectedJobItem = _jobItemTransforms.Aggregate((job1Tr, job2Tr) =>
      {
        var job1DistanceToFrame = (job1Tr.position - selectedJobFramePos).magnitude;
        var job2DistanceToFrame = (job2Tr.position - selectedJobFramePos).magnitude;

        return job1DistanceToFrame < job2DistanceToFrame ? job1Tr : job2Tr;
      });


    }

    public void Update()
    {
        try {
      scrollRect.velocity = lockSpeedMultiplier * new Vector2(0, 
        (selectedJobFrame.position - SelectedJobItem.position).y);
        }
        catch(NullReferenceException e)
        {

        }


    }
  }
}