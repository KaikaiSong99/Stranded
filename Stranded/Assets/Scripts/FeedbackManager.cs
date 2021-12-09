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

    private List<GameObject> feedbackObjects;

    public void Start()
    {
        feedbackObjects = new List<GameObject>();
    }

    public IEnumerator ShowFeedback(int roundScore, List<Character> characters)
    {
        Debug.Log("Show Feedback");

        score.text = String.Format("Your score is: {0}", roundScore);
        // score.text = "h";
        Debug.Log(score.text);

        foreach (var character in characters) 
        {
            Debug.Log("Instantiate");
            var gameObject = Instantiate<GameObject>(feedbackPrefab);
            gameObject.transform.SetParent(feedbackVGroup.transform);

            FeedbackStats feedbackStats = gameObject.GetComponent<FeedbackStats>();
            feedbackStats.Show(character);

            feedbackObjects.Add(gameObject);
            yield return new WaitForSeconds(3);

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
