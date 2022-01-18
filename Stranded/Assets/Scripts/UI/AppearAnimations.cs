using System;
using UnityEngine;
using Util;

namespace UI
{
  public static class AppearAnimations
  {
    public static void GrowWidth(this MonoBehaviour uiElement, float duration, RectTransform rectTransform,
      Action @continue)
    {
        uiElement.InterpolateSinus(duration, y =>
        {
          rectTransform.sizeDelta = new Vector2(rectTransform.rect.width * y, rectTransform.sizeDelta.y);
        }, @continue);
    }
    
    public static void FadeIn(this MonoBehaviour uiElement, float duration, CanvasGroup canvasGroup,
      Action @continue)
    {
      uiElement.InterpolateSinus(duration, y =>
      {
        canvasGroup.alpha = y;
      }, @continue);
    }
  }
}