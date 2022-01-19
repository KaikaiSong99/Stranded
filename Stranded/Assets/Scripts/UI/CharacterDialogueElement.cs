using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  [RequireComponent(typeof(CanvasGroup))]
  [RequireComponent(typeof(RectTransform))]
  public class CharacterDialogueElement : MonoBehaviour, IAppearElement
  {
    public float appearDuration = 0.5f;
    public BasicTextElement characterNameElement;
    public BasicTextElement characterSpeechElement;
    public Image characterPortraitElement;

    public RectTransform RectTransform { get; private set; }
    
    public Action Continue { get; set; }

    public CanvasGroup CanvasGroup { get; private set; }
    
    public void Appear()
    {
      this.FadeIn(appearDuration, CanvasGroup, () =>
      {
        characterNameElement.LinkTo(characterSpeechElement, Continue);
        characterNameElement.Appear();
      });
    }

    public void AppearImmediately()
    {
      CanvasGroup.alpha = 1;
      characterNameElement.LinkToImmediate(characterSpeechElement, Continue);
      characterNameElement.AppearImmediately();
    }

    public void Populate(Character character, string feedbackText)
    {
      Debug.Log($"characterNameElement: {characterNameElement}");
      Debug.Log($"character: {character}");
      
      characterNameElement.Text = character.name;
      characterPortraitElement.sprite = character.portrait;
      characterSpeechElement.Text = feedbackText;
    }

    private void Start()
    {
      RectTransform = GetComponent<RectTransform>();
      CanvasGroup = GetComponent<CanvasGroup>();
      CanvasGroup.alpha = 0;
    }
  }
}