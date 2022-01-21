using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  [RequireComponent(typeof(RectTransform))]
  [RequireComponent(typeof(CanvasGroup))]
  public class BasicTextElement : MonoBehaviour, IAppearElement
  {
    public float bottomPadding = 10f;

    public Text textComponent;

    public float letterAppearDuration = 0.04f;

    public float afterCompletionDuration = 0.3f;

    public string Text
    {
      get => textComponent.text;
      set => textComponent.text = value;
    }

    public RectTransform RectTransform { get; private set; }

    public Action Continue { get; set; }

    private CanvasGroup _canvasGroup;
    
    public void Appear()
    {
      IEnumerator SimpleLetterAppearAnimation()
      {
        var fullText = Text;
        Text = "";
        _canvasGroup.alpha = 1;
        
        for (var l = 0; l <= fullText.Length; l++)
        {
          Text = fullText.Substring(0, l);
          yield return new WaitForSeconds(letterAppearDuration);
        }
        
        yield return new WaitForSeconds(afterCompletionDuration);
        Continue();
      }

      StartCoroutine(SimpleLetterAppearAnimation());
    }

    public void AppearImmediately()
    {
      _canvasGroup.alpha = 1;
      Continue();
    }

    private void Start()
    {
      RectTransform = GetComponent<RectTransform>();
      _canvasGroup = GetComponent<CanvasGroup>();
      _canvasGroup.alpha = 0;
    }
  }
}