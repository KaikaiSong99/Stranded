using System;
using UnityEngine;
using Util;

namespace UI
{
  public class TimeBar : MonoBehaviour
  {
    [SerializeField]
    private float progressNormalized = 1.0f;
    public float showAnimationDuration = 0.5f;

    public float ProgressNormalized
    {
      get => progressNormalized;
      set
      {
        progressNormalized = value;
        UpdateBarSizeBasedOnProgress();
      }
    }
    
    public RectTransform growingBarElement;

    private RectTransform _rectTransform;
    private float _barStartWidth;
    private float _frameStartHeight;

    public void Appear()
    {
      this.InterpolateSinus(showAnimationDuration, y =>
      {
        AdjustFramePosY(y * _frameStartHeight - _frameStartHeight);
      });
    }

    public void Disappear()
    {
      this.InterpolateSinus(showAnimationDuration, y =>
      {
        AdjustFramePosY(y * -_frameStartHeight);
      });
    }

    private void Start()
    {
      _rectTransform = GetComponent<RectTransform>();
      _barStartWidth = growingBarElement.sizeDelta.x;
      _frameStartHeight = _rectTransform.sizeDelta.y;
    }

    private void UpdateBarSizeBasedOnProgress()
    {
      AdjustBarSizeX(_barStartWidth * progressNormalized);
    }

    private void AdjustBarSizeX(float x)
    {
      growingBarElement.sizeDelta = new Vector2(x, growingBarElement.sizeDelta.y);
    }

    private void AdjustFramePosY(float y)
    {
      _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, y);
    }
  }
}