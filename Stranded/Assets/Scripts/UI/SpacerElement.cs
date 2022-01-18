using System;
using UnityEngine;

namespace UI
{
  [RequireComponent(typeof(RectTransform))]
  public class SpacerElement : MonoBehaviour, IAppearElement
  {
    public float spacing;

    public RectTransform RectTransform { get; private set; }
    
    public Action Continue { get; set; }
    
    public void Appear()
    {
      Continue();
    }

    public void AppearImmediately()
    {
      Continue();
    }

    private void Start()
    {
      OnValidate();
    }

    private void OnValidate()
    {
      if (RectTransform == null) RectTransform = GetComponent<RectTransform>();
      RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, spacing);
    }
  }
}