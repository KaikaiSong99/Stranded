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
    public GameObject jobElementPrefab;
    public GameObject paragraphPrefab;
    public GameObject spacerPrefab;

    private RectTransform _rootRectTransform;
    
    /// <summary>
    /// Determines where on the y axis within the space of the root rect transform the next UI element will be
    /// instantiated. Throughout the build process this value grows more and more negative.
    /// </summary>
    private float _spawnPointer;

    private void Start()
    {
      _rootRectTransform = GetComponent<RectTransform>();
      _spawnPointer = -spacing;
    }

    public IEnumerator ConstructAssignmentPhaseUI(RoundManager roundManager, Action<AssignmentPhaseUIInfo> use = null)
    {
      var dilemma = roundManager.Dilemma;
      var appearElements = new List<IAppearElement>();
      var jobElements = new List<JobElement>();

      void AddToAppearElements(IAppearElement el)
      {
        appearElements.Add(el);
      }
      
      yield return InstantiateH1Element($"Chapter {dilemma.round}", AddToAppearElements);
      yield return InstantiateImageElement(dilemma.sprite, AddToAppearElements);
      yield return InstantiateH2Element(dilemma.title, AddToAppearElements);
      
      foreach (var p in dilemma.description.Split('\n'))
      {
        yield return InstantiateParagraphElement(p, AddToAppearElements);
      }
      
      foreach (var job in dilemma.jobs)
      {
        yield return InstantiateSpacerElement(AddToAppearElements);
        yield return InstantiateParagraphElement("Someone needs to...", AddToAppearElements);
        yield return InstantiateJobElement(job, roundManager, jobEl =>
        {
          AddToAppearElements(jobEl);
          jobElements.Add(jobEl);
        });
      }
      
      
      use?.Invoke(new AssignmentPhaseUIInfo {AppearElements = appearElements, JobElements = jobElements}); 
    }
  
    public IEnumerator ConstructFeedbackPhaseUI(RoundManager roundManager, Action use = null)
    {
      var appearElements = new List<IAppearElement>();
      var dilemma = roundManager.Dilemma;
      
      void AddToAppearElements(IAppearElement el)
      {
        appearElements.Add(el);
      }

      yield return InstantiateParagraphElement(PickDilemmaSuccessText(roundManager), AddToAppearElements);
      
    }

    public void CreateStoryTextUI(StoryPoint storyPoint)
    {
      
    }

    private string PickDilemmaSuccessText(RoundManager roundManager)
    {
      var dilemma = roundManager.Dilemma;
      var round = roundManager.Round;
      if (round.Succeeded)
      {
        return dilemma.successText;
      }
      
      return round.PartiallySucceeded ? dilemma.partialSuccessText : dilemma.failureText;
    }

    private IEnumerator InstantiateJobElement(Job job, RoundManager roundManager, Action<JobElement> use = null)
    {
      return InstantiateGenericUIElement(jobElementPrefab, obj =>
      {
        var jobElement = obj.GetComponent<JobElement>();
        jobElement.Populate(job, roundManager);
        
        use?.Invoke(jobElement);
      });
    }

    private IEnumerator InstantiateSpacerElement(Action<SpacerElement> use = null)
    {
      return InstantiateGenericUIElement(spacerPrefab, obj => 
        use?.Invoke(obj.GetComponent<SpacerElement>()));
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