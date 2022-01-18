using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class UIBuilder : MonoBehaviour
  {
    public float spacing = 20f;
    
    public GameObject h1Prefab;
    public GameObject h2Prefab;
    public GameObject imagePrefab;
    public GameObject paragraphPrefab;

    private RectTransform _rootRectTransform;
    
    /// <summary>
    /// Determines where on the y axis within the space of the root rect transform the next UI element will be
    /// instantiated. Throughout the build process this value grows more and more negative.
    /// </summary>
    private float _spawnPointer;

    /// <summary>
    /// I know, this feels super hacky, but it is otherwise really hard to get a return value from a coroutine,
    /// even if you know for sure the routine has finished. 
    /// </summary>
    private object _returnObject;

    private void Start()
    {
      _rootRectTransform = GetComponent<RectTransform>();
      _spawnPointer = -spacing;
    }

    public AssignmentPhaseUIInfo ConstructAssignmentPhaseUI(Dilemma dilemma)
    {
      StartCoroutine(ConstructAssignmentPhaseUIRoutine(dilemma));

      return null;
    }

    private IEnumerator ConstructAssignmentPhaseUIRoutine(Dilemma dilemma)
    {
      yield return InstantiateH1Element($"Chapter {dilemma.round}");
      yield return InstantiateImageElement(dilemma.sprite);
      yield return InstantiateH2Element(dilemma.title);
      foreach (var p in dilemma.description.Split('\n'))
      {
        yield return InstantiateParagraphElement(p);
      }
    }
  
    public void ConstructFeedbackPhaseUI(Round round, 
      Dictionary<Job, Dictionary<Character, string>> feedbackDictionary)
    {
    
    }

    public void CreateStoryTextUI(StoryPoint storyPoint)
    {
      
    }

    private IEnumerator InstantiateH1Element(string text)
    {
      return InstantiateBasicTextElement(text, h1Prefab);
    }

    private IEnumerator InstantiateH2Element(string text)
    {
      return InstantiateBasicTextElement(text, h2Prefab);
    }

    private IEnumerator InstantiateParagraphElement(string text)
    {
      return InstantiateBasicTextElement(text, paragraphPrefab);
    }

    private IEnumerator InstantiateBasicTextElement(string text, GameObject textElPrefab)
    {
      yield return InstantiateGenericUIElement(textElPrefab, obj =>
      {
        var textElement = obj.GetComponent<BasicTextElement>();
        textElement.Text = text;

        _spawnPointer -= textElement.bottomPadding;
      });
      
      _returnObject = ((GameObject) _returnObject).GetComponent<BasicTextElement>();
    }

    private IEnumerator InstantiateImageElement(Sprite sprite)
    {
      yield return InstantiateGenericUIElement(imagePrefab, obj =>
      {
        var imageElement = obj.GetComponent<BasicImageElement>();
        imageElement.Sprite = sprite;
        imageElement.RefreshAspectRatio();
      });

      _returnObject = ((GameObject) _returnObject).GetComponent<BasicImageElement>();;
    }
    
    private IEnumerator InstantiateGenericUIElement(GameObject prefab, Action<GameObject> beforeSpacing = null)
    {
      Debug.Log($"Try to instantiate {prefab}");
      var obj = Instantiate(prefab, _rootRectTransform);
      
      var rectTransform = obj.GetComponent<RectTransform>();
      rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, _spawnPointer);
      
      beforeSpacing?.Invoke(obj);

      yield return null;

      Debug.Log(rectTransform.rect.height);
      _spawnPointer -= rectTransform.sizeDelta.y + spacing;

      _returnObject = obj;
    }
  }
}