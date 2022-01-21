using System;
using UnityEngine;

namespace UI
{
  public interface IAppearElement
  {
    RectTransform RectTransform { get; }

    /// <summary>
    /// Should be called after the element has finished appearing. Can be assumed to be not null by the time Appear()
    /// is called.
    /// </summary>
    Action Continue { get; set; }
  
    void Appear();

    /// <summary>
    /// This is used for debugging to skip the appear step.
    /// </summary>
    void AppearImmediately();
  }
}