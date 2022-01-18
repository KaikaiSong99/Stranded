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
    private RoundManager _roundManager;
    
    public void Appear()
    {
      this.FadeIn(appearDuration, CanvasGroup, () =>
      {
        jobNameElement.LinkTo(jobDescriptionElement, Continue);
        jobNameElement.Appear();
      });
    }

    public void AppearImmediately()
    {
      CanvasGroup.alpha = 1;
      jobNameElement.LinkToImmediate(jobDescriptionElement, Continue);
      jobNameElement.AppearImmediately();
    }

    public void Populate(Job job, RoundManager roundManager)
    {
      _job = job;
      _roundManager = roundManager;
      
      jobNameElement.Text = job.name;
      jobDescriptionElement.Text = job.description;
      jobIconElement.sprite = job.jobIcon;
    }

    public void Refresh()
    {
      characterIconElement.sprite = _roundManager.Round.PickedCharacters.TryGetValue(_job, out var assignedCharacter) 
        ? assignedCharacter.portrait : noCharacterSelectedIcon;
    }
      

    public void OnPress()
    {
      Debug.Log($"Pressed on {_job.name}.");
      _roundManager.assignmentManager.Display(_roundManager.Dilemma.characters, _job, _roundManager.Round);
    }

    private void Start()
    {
      RectTransform = GetComponent<RectTransform>();
      CanvasGroup = GetComponent<CanvasGroup>();
      CanvasGroup.alpha = 0;
      Refresh();
      this.SetInteractable(false);
    }
  }
}