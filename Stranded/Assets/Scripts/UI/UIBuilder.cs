using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

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

    public IEnumerator ConstructAssignmentPhaseUI(Dilemma dilemma, Action<List<IAppearElement>> use = null)
    {
      var appearElements = new List<IAppearElement>();

      void AddToList(IAppearElement el)
      {
        appearElements.Add(el);
      }
      
      yield return InstantiateH1Element($"Chapter {dilemma.round}", AddToList);
      yield return InstantiateImageElement(dilemma.sprite, AddToList);
      yield return InstantiateH2Element(dilemma.title, AddToList);
      foreach (var p in dilemma.description.Split('\n'))
      {
        yield return InstantiateParagraphElement(p, AddToList);
      }
      
      use?.Invoke(appearElements);
    }
  
    public void ConstructFeedbackPhaseUI(Round round, 
      Dictionary<Job, Dictionary<Character, string>> feedbackDictionary)
    {
    
    }

    public void CreateStoryTextUI(StoryPoint storyPoint)
    {
      
    }

    private IEnumerator InstantiateH1Element(string text, Action<BasicTextElement> use = null)
    {
      return InstantiateBasicTextElement(text, h1Prefab, use);
    }

    private IEnumerator InstantiateH2Element(string text, Action<BasicTextElement> use = null)
    {
      return InstantiateBasicTextElement(text, h2Prefab, use);
    }

    private IEnumerator InstantiateParagraphElement(string text, Action<BasicTextElement> use = null)
    {
      return InstantiateBasicTextElement(text, paragraphPrefab, use);
    }

    private IEnumerator InstantiateBasicTextElement(string text, GameObject textElPrefab, 
      Action<BasicTextElement> use = null)
    {
      yield return InstantiateGenericUIElement(textElPrefab, obj =>
      {
        var textElement = obj.GetComponent<BasicTextElement>();
        textElement.Text = text;
        use?.Invoke(textElement);

        _spawnPointer -= textElement.bottomPadding;
      });
    }

    private IEnumerator InstantiateImageElement(Sprite sprite, Action<BasicImageElement> use = null)
    {
      yield return InstantiateGenericUIElement(imagePrefab, obj =>
      {
        var imageElement = obj.GetComponent<BasicImageElement>();
        imageElement.Sprite = sprite;
        imageElement.RefreshAspectRatio();
        use?.Invoke(imageElement);
      });
    }
    
    private IEnumerator InstantiateGenericUIElement(GameObject prefab, Action<GameObject> beforeSpacing = null)
    {
      var obj = Instantiate(prefab, _rootRectTransform);
      
      var rectTransform = obj.GetComponent<RectTransform>();
      rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, _spawnPointer);
      
      beforeSpacing?.Invoke(obj);

      yield return null;

      _spawnPointer -= rectTransform.sizeDelta.y + spacing;
    }
  }
}