using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Model;

public class FeedbackManager : MonoBehaviour
{
    public Text score;
    public GameObject feedbackPrefab;
    public VerticalLayoutGroup feedbackVGroup;

    private List<GameObject> feedbackObjects = new List<GameObject>();

    public void Start()
    {
    }

    public IEnumerator ShowFeedback(int roundScore, List<Character> characters)
    {
        score.text = String.Format("Your score for this round: {0}", roundScore);
        
        foreach (var character in characters) 
        {
            var gameObject = Instantiate<GameObject>(feedbackPrefab, transform);
            gameObject.transform.SetParent(feedbackVGroup.transform);
        
            FeedbackStats feedbackStats = gameObject.GetComponent<FeedbackStats>();
            feedbackStats.Show(character);
            Debug.Log(feedbackObjects);
            feedbackObjects.Add(gameObject);
            
            yield return new WaitForSeconds(3f);
        }
    
        yield return StartCoroutine(Cleanup());
    }


    private IEnumerator Cleanup() {
        yield return new WaitForSeconds(3);

        foreach (var gameObject in feedbackObjects) 
        {
            Destroy(gameObject);
        }
        feedbackObjects.Clear();

        yield return null;

    }

}