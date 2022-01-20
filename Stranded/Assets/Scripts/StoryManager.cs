using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Model;
using TMPro;

public class StoryManager : MonoBehaviour
{
    // When the round is done call onRoundEnd?.Invoke(null)
    public static event Action<Round> onRoundEnd;

    public Image storyArt;
    public TextMeshProUGUI storyText;
    public TextMeshProUGUI endText;

    private float nextTextDelay = 0f;
    public float letterAppearDuration = 0.04f;
    public float periodDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onRoundInit += Play;
        endText.gameObject.SetActive(false);
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
            StartCoroutine(PlayStory(storyPoint));
        }

    }

    public IEnumerator PlayStory(StoryPoint story)
    {
        storyArt.sprite = story.storyArt;
        nextTextDelay = story.playTime;
        
        foreach (var text in story.storyText)
        {
            for (var l = 0; l < text.Length; l++)
            {
                if (text[l] == '\n') 
                    yield return new WaitForSeconds(periodDelay);

                storyText.text = text.Substring(0, l);
                yield return new WaitForSeconds(letterAppearDuration);
            }

            storyText.text = text;
            yield return new WaitForSeconds(nextTextDelay);
        }

        if (story.isEndPoint) 
        {
            endText.gameObject.SetActive(true);
            yield return new WaitForSeconds(nextTextDelay);
        }


        onRoundEnd?.Invoke(null);
    }

    void OnDestroy() {
        GameManager.onRoundInit -= Play;
    }
}
