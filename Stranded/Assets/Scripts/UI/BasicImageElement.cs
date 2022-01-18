using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UI
{
  [RequireComponent(typeof(RectTransform))]
  [RequireComponent(typeof(CanvasGroup))]
  public class BasicImageElement : MonoBehaviour, IAppearElement
  {
    public Image image;

    public AspectRatioFitter aspectRatioFitter;

    public float appearDuration = 0.5f;

    public Sprite Sprite
    {
      get => image.sprite;
      set => image.sprite = value;
    }

    public RectTransform RectTransform { get; private set; }
    
    public Action Continue { get; set; }

    private CanvasGroup _canvasGroup;
    
    public void Appear()
    {
      this.FadeIn(appearDuration, _canvasGroup, Continue);
    }

    private void Start()
    {
      RectTransform = GetComponent<RectTransform>();
      _canvasGroup = GetComponent<CanvasGroup>();
      _canvasGroup.alpha = 0;
    }

    public void RefreshAspectRatio()
    {
      var texture = image.sprite.texture;
      Assert.IsNotNull(texture); // if raised, probably Start is called before Sprite assignment
      aspectRatioFitter.aspectRatio = (float) texture.width / texture.height;
    }
  }
}