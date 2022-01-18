using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UI
{
  [RequireComponent(typeof(RectTransform))]
  public class BasicImageElement : MonoBehaviour, IAppearElement
  {
    public Image image;
    
    public AspectRatioFitter aspectRatioFitter;

    public Sprite Sprite
    {
      get => image.sprite;
      set => image.sprite = value;
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

    public void RefreshAspectRatio()
    {
      var texture = image.sprite.texture;
      Assert.IsNotNull(texture); // if raised, probably Start is called before Sprite assignment
      aspectRatioFitter.aspectRatio = (float) texture.width / texture.height;
    }
  }
}