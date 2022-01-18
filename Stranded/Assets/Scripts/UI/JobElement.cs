using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  [RequireComponent(typeof(CanvasGroup))]
  [RequireComponent(typeof(RectTransform))]
  public class JobElement : MonoBehaviour, IAppearElement, IHasCanvasGroup
  {
    public float appearDuration = 0.5f;
    public BasicTextElement jobNameElement;
    public BasicTextElement jobDescriptionElement;
    public Image jobIconElement;
    public Image characterIconElement;
    public Sprite noCharacterSelectedIcon;
    
    public RectTransform RectTransform { get; private set; }
    
    public Action Continue { get; set; }

    public CanvasGroup CanvasGroup { get; private set; }

    private Job _job;
    
    public void Appear()
    {
      this.FadeIn(appearDuration, CanvasGroup, () =>
      {
        jobNameElement.LinkTo(jobDescriptionElement, Continue);
        jobNameElement.Appear();
      });
    }

    public void Populate(Job job)
    {
      _job = job;
      jobNameElement.Text = job.name;
      jobDescriptionElement.Text = job.description;
      jobIconElement.sprite = job.jobIcon;
    }

    public void OnPress()
    {
      Debug.Log($"Pressed on {_job.name}.");
    }

    private void Start()
    {
      RectTransform = GetComponent<RectTransform>();
      CanvasGroup = GetComponent<CanvasGroup>();
      CanvasGroup.alpha = 0;
      characterIconElement.sprite = noCharacterSelectedIcon;
    }
  }
}