using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  [RequireComponent(typeof(CanvasGroup))]
  [RequireComponent(typeof(RectTransform))]
  public class FeedbackJobElement : MonoBehaviour, IAppearElement
  {
    public float appearDuration = 0.5f;
    public BasicTextElement jobNameElement;
    public Image jobIconElement;
    public Image successIcon;

    public Sprite successSprite;
    public Sprite failureSprite;
    
    public RectTransform RectTransform { get; private set; }
    
    public Action Continue { get; set; }

    public CanvasGroup CanvasGroup { get; private set; }

    public void Appear()
    {
      this.FadeIn(appearDuration, CanvasGroup, () =>
      {
        jobNameElement.Continue = Continue;
        jobNameElement.Appear();
      });
    }

    public void AppearImmediately()
    {
      CanvasGroup.alpha = 1;
      jobNameElement.Continue = Continue;
      jobNameElement.AppearImmediately();
    }

    public void Populate(Job job, RoundManager roundManager)
    {
      jobNameElement.Text = job.name;
      jobIconElement.sprite = job.jobIcon;
      successIcon.sprite = roundManager.Round.IsJobCorrectlyAssigned(job) ? successSprite : failureSprite;
    }

    private void Start()
    {
      RectTransform = GetComponent<RectTransform>();
      CanvasGroup = GetComponent<CanvasGroup>();
      CanvasGroup.alpha = 0;
    }
  }
}