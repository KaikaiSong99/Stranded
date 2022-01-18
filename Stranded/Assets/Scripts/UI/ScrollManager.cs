using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Util;

namespace UI
{
  [RequireComponent(typeof(ScrollRect))]
  public class ScrollManager : MonoBehaviour
  {
    private ScrollRect _scrollRect;
    private ScrollRect.MovementType _scrollRectStartMovementType;

    public float timeBetween = 0.3f;
    public float focusAnimationDuration = 0.5f;
    public float bottomPadding = 80f;
    public bool disableScroll = true;
    public bool immediateMode = false;
  
    private void Start()
    {
      _scrollRect = GetComponent<ScrollRect>();
      _scrollRectStartMovementType = _scrollRect.movementType;
    }

    public void ScrollThrough(IEnumerable<IAppearElement> appearElementsEnumerable, Action atEnd = null)
    {
      var appearElements = appearElementsEnumerable.ToArray();
    
      // Assume that there is at least one appearElement
      Assert.IsTrue(appearElements.Length > 0);
    
      // Prepare scroll rect component, disable input
      _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
      _scrollRect.vertical = !disableScroll;

      // Make every element's continue function call the appear of the next.
      for (var i = 0; i < appearElements.Length - 1; i++)
      {
        var next = i + 1;
        appearElements[i].Continue = () => this.Delay(timeBetween, () =>
        {
          Debug.Log(next);
          MakeAppearAndFocused(appearElements[next]);
        });
      }

      // Make the last element's continue function call Finish and end action
      appearElements[appearElements.Length - 1].Continue = () =>
      {
        atEnd?.Invoke();
        Finish();
      };
    
      // Make the first element appear
      MakeAppearAndFocused(appearElements[0]);
    }

    private void MakeAppearAndFocused(IAppearElement appearElement)
    {
      AddNewElementToScrollView(appearElement);
      FocusOnElement();
      MakeAppear(appearElement);
    }

    private void AddNewElementToScrollView(IAppearElement appearElement)
    {
      var newHeight = -appearElement.RectTransform.anchoredPosition.y 
                      + appearElement.RectTransform.rect.height + bottomPadding;

      var content = _scrollRect.content;
      content.sizeDelta = new Vector2(content.sizeDelta.x, newHeight);
    }
  
    private void FocusOnElement()
    {
      var startPosition = _scrollRect.verticalNormalizedPosition;
      var targetPosition = 0f;
      var difference = targetPosition - startPosition;
    
      this.InterpolateSinus(focusAnimationDuration, y =>
      {
        _scrollRect.verticalNormalizedPosition = startPosition + difference * y;
      });
    }

    private void MakeAppear(IAppearElement appearElement)
    {
      if (!immediateMode)
      {
        appearElement.Appear();
      }
      else
      {
        appearElement.AppearImmediately();
      }
    }

    private void Finish()
    {
      _scrollRect.movementType = _scrollRectStartMovementType;
      _scrollRect.vertical = true;
    }
  }
}