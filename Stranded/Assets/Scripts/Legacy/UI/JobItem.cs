using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class JobItem : MonoBehaviour
  {
    public Job job;

    public Image image;

    private void Start()
    {
      image.sprite = job.jobIcon;
    }
  }
}