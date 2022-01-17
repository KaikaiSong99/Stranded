using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  [RequireComponent(typeof(RectTransform))]
  public class BasicTextElement : MonoBehaviour, IAppearElement
  {
    public float bottomPadding = 10f;

    public Text textComponent;

    public string Text
    {
      get => textComponent.text;
      set => textComponent.text = value;
    }

    public RectTransform RectTransform { get; private set; }

    public Action Continue { get; set; }
    
    public void Appear()
    {
      // TODO
      Continue();
    }

    private void Start()
    {
      RectTransform = GetComponent<RectTransform>();
    }
  }
}