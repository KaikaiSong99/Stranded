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
    private float _spawnPointer = 0f;

    private void Start()
    {
      _rootRectTransform = GetComponent<RectTransform>();
    }

    public AssignmentPhaseUIInfo ConstructAssignmentPhaseUI(Dilemma dilemma, int round)
    {
      InstantiateH1Element($"Chapter {round}");
      InstantiateImageElement(dilemma.sprite);
      InstantiateH2Element(dilemma.title);
      

      return new AssignmentPhaseUIInfo();
    }
  
    public void ConstructFeedbackPhaseUI(Round round, 
      Dictionary<Job, Dictionary<Character, string>> feedbackDictionary)
    {
    
    }

    public void CreateStoryTextUI(StoryPoint storyPoint)
    {
      
    }

    private BasicTextElement InstantiateH1Element(string text)
    {
      return InstantiateBasicTextElement(text, h1Prefab);
    }

    private BasicTextElement InstantiateH2Element(string text)
    {
      return InstantiateBasicTextElement(text, h2Prefab);
    }

    private BasicTextElement InstantiateParagraphElement(string text)
    {
      return InstantiateBasicTextElement(text, paragraphPrefab);
    }

    private BasicTextElement InstantiateBasicTextElement(string text, GameObject textElPrefab)
    {
      var obj = InstantiateUIElement(textElPrefab);
      var textElement = obj.GetComponent<BasicTextElement>();
      textElement.Text = text;

      _spawnPointer += textElement.bottomPadding;
      
      return textElement;
    }

    private BasicImageElement InstantiateImageElement(Sprite sprite)
    {
      var obj = InstantiateUIElement(imagePrefab);
      var imageElement = obj.GetComponent<BasicImageElement>();
      imageElement.Sprite = sprite;

      return imageElement;
    }
    
    private GameObject InstantiateUIElement(GameObject prefab)
    {
      var obj = Instantiate(prefab, _rootRectTransform);
      var rectTransform = obj.GetComponent<RectTransform>();
      rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, _spawnPointer);

      _spawnPointer += rectTransform.sizeDelta.y + spacing;
      
      return obj;
    }
  }
}