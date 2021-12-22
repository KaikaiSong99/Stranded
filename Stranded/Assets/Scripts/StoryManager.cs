using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Model;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    // When the round is done call onRoundEnd?.Invoke(null)
    public static event Action<Round> onRoundEnd;

    public Image storyArt;
    public Text storyText;

    public int nextTextDelay;
    public int timeLeft;

    public SceneTransition transitionController;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onRoundInit += Play;

        storyText.text = "";
        
    }
    // Start this story round (is called from GameManager)
    // Cast the parameters to StoryPoint type with "StoryPoint s = parameters as StoryPoint"
    // Could also check if it is a StoryPoint and return if not
    public void Play(BaseSceneParameter parameters)
    {

        var storyPoint = parameters as StoryPoint;

        if (storyPoint != null)
        {
            Debug.Log($"Begin the story with {storyPoint.characters}");
            StartCoroutine(PlayStory(storyPoint));
        }

    }

    public IEnumerator PlayStory(StoryPoint story)
    {

        timeLeft = (int) Math.Round(story.playTime);
        storyArt.sprite = story.storyArt;
        
        foreach (var text in story.storyText)
        {
            storyText.text = text;
            timeLeft -= nextTextDelay;
            yield return new WaitForSeconds(nextTextDelay);
        }

        if (timeLeft < 0)
        {
            timeLeft = 3;
        }
            
        yield return new WaitForSeconds(timeLeft);

        yield return StartCoroutine(transitionController.FadeOut());
        
        onRoundEnd?.Invoke(null);
    }

    void OnDestroy() {
        GameManager.onRoundInit -= Play;
    }
}
