using System.Collections.Generic;
using System.Linq;
using Temp;
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
  
    // probably not use heights but extract data from rect transforms directly
    private float _scrollContentHeight;
    private float _previousScrollContentHeight;
  
    // debug variable
    public float verticalNormalizedPosition;
  
    public float timeBetween = 0.3f;
    public float focusAnimationDuration = 0.5f;
    public bool disableScroll = true;
  
    private void Start()
    {
      _scrollRect = GetComponent<ScrollRect>();
      _scrollRectStartMovementType = _scrollRect.movementType;
    
      // TODO remove this
    
      this.Delay(1f, () => ScrollThrough(
        gameObject.GetComponentsInChildren<ExampleAppearElement>()));
    }

    private void Update()
    {
      // TODO remove this
      verticalNormalizedPosition = _scrollRect.verticalNormalizedPosition;
      if (Input.GetKeyDown(KeyCode.Q))
      {
        _scrollRect.verticalNormalizedPosition = 0.5f;
      }
    }

    public void ScrollThrough(IEnumerable<IAppearElement> appearElementsEnumerable)
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
          MakeAppearAndFocused(appearElements[next]));
      }

      // Make the last element's continue function call Finish
      appearElements[appearElements.Length - 1].Continue = Finish;
    
      // Make the first element appear
      MakeAppearAndFocused(appearElements[0]);
    }

    private void MakeAppearAndFocused(IAppearElement appearElement)
    {
      AddNewElementToScrollView(appearElement);
      FocusOnElement();
      appearElement.Appear();
    }

    private void AddNewElementToScrollView(IAppearElement appearElement)
    {
      _previousScrollContentHeight = _scrollContentHeight;
      _scrollContentHeight += appearElement.RectTransform.rect.height;

      var content = _scrollRect.content;
      content.sizeDelta = new Vector2(content.sizeDelta.x, _scrollContentHeight);
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

    private void Finish()
    {
      _scrollRect.movementType = _scrollRectStartMovementType;
      _scrollRect.vertical = true;
    }
  }
}