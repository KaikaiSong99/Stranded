using System;
using UI;
using UnityEngine;
using Util;

namespace Temp
{
  [RequireComponent(typeof(RectTransform))]
  public class ExampleAppearElement : MonoBehaviour, IAppearElement
  {
    public float appearDuration = 0.5f;
    
    public RectTransform RectTransform { get; private set; }

    public Action Continue { get; set; }

    private float _fullWidth;

    private void Start()
    {
      RectTransform = GetComponent<RectTransform>();
      _fullWidth = RectTransform.sizeDelta.x;
      UpdateWidth(0);
    }

    public void Appear()
    {
      this.InterpolateSinus(appearDuration, y =>
      {
        UpdateWidth(_fullWidth * y);
      }, Continue);
    }

    private void UpdateWidth(float width)
    {
      RectTransform.sizeDelta = new Vector2(width, RectTransform.sizeDelta.y);
    }
  }
}