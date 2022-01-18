using System;
using System.Collections;
using System.Collections.Generic;
using Legacy;
using UnityEngine;
using Model;
using UI;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Util;

public class RoundManager : MonoBehaviour
{
    // When the round is done call onRoundEnd?.Invoke(Round round)
    public static event Action<Round> onRoundEnd;

    public Dilemma Dilemma { get; private set; }
    
    public Round Round { get; private set; }

    //Dictionary containing feedback dialogue per characters, since there is a case where a single character has more than one piece of feedback, the dialogue is stored in a list.
    public Dictionary<Job, Dictionary<Character, string>> FeedbackDictionary { get; private set; }

    // Different timers for the different phases
    public float introTime = 5;
    public float executionTime = 5;
    public float feedbackTime = 30;

    public UIBuilder uiBuilder;

    public ScrollManager scrollManager;
    
    public JobManager jobManager;
  
    public float timeLeft;

    public Text dilemmaTitle;
    public Image dilemmaSprite;
    //public GameObject overviewUI;
    public GameObject jobsOverview;

    public FeedbackManager feedbackManager;

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.onRoundInit += Play;
    }

    // public void ShowOverview()
    // {
    //     dilemmaTitle.text = dilemma.title;
    //     dilemmaSprite.sprite = dilemma.sprite;
    // }
    //
    // public void ShowAssignmentOverview()
    // {
    //     //overviewUI.gameObject.SetActive(false);
    //     jobsOverview.gameObject.SetActive(true);
    //     jobManager.roundManager = this;
    //     jobManager.CreateCards(dilemma);
    //
    //     Debug.Log($" Job count: {dilemma.jobs.Count}");
    // }

    // Start this round (is called from GameManager)
    // Cast the parameters to Dilemma type with "Dilemma s = parameters as Dilemma"
    // Could also check if it is a Dilemma and return if not
    private void Play(BaseSceneParameter parameters)
    {
        Dilemma = parameters as Dilemma;
        Assert.IsNotNull(Dilemma);
        
        Round = new Round(Dilemma.isCounted, 0, Dilemma);
        FeedbackDictionary = new Dictionary<Job, Dictionary<Character,String>>();
        foreach (var job in Dilemma.jobs)
        {
            FeedbackDictionary.Add(job, new Dictionary<Character, String>());
        }
        
        PlayAssignmentPhase();

        // StartCoroutine(PlayIntroPhase());
    }

    // Sequence of operation:
    // 1. Construct all assignment phase UI elements & make them appear.
    // 2. Player goes through job assignment process (timer starts)
    // 3. Construct all feedback phase UI elements & make them appear.
    // 4. End

    public void PlayAssignmentPhase()
    {
        Debug.Log($"Assignment phase for round {Dilemma.round} has started.");
        StartCoroutine(uiBuilder.ConstructAssignmentPhaseUI(Dilemma, info =>
        {
            scrollManager.ScrollThrough(info.AppearElements);
        }));
    }
    
    // public IEnumerator PlayIntroPhase()
    // {
    //     Debug.Log("Intro has started");
    //     timeLeft = introTime;
    //     ShowOverview();
    //     yield return StartCoroutine(Timer(() => PlayAssignmentPhase()));
    // }

    // public IEnumerator PlayAssignmentPhase()
    // {
    //     ShowAssignmentOverview();
    //     Debug.Log("Assignment has started");
    //     timeLeft = dilemma.playTime;
    //     yield return StartCoroutine(Timer(() => PlayExecutionPhase()));
    // }

    // public IEnumerator PlayExecutionPhase()
    // {
    //     Debug.Log("Execution has started");
    //     timeLeft = executionTime;
    //     ComputeCorrect();
    //     yield return StartCoroutine(Timer(() => PlayFeedbackPhase()));
    // }

    // public IEnumerator PlayFeedbackPhase()
    // {
    //     Debug.Log("Feedback has started");
    //     timeLeft = feedbackTime;
    //     feedbackManager.gameObject.SetActive(true);
    //     StartCoroutine(feedbackManager.Show(feedback, round.PickedCharacters));
    //     yield return StartCoroutine(Timer(() => null));
    //     onRoundEnd?.Invoke(round);
    // }

    private IEnumerator Timer(Func<IEnumerator> func)
    {
        while (timeLeft > 0)
        {
            --timeLeft;
            yield return new WaitForSeconds(1f);
        }

        yield return func();
    }

    public void AssignCharacterToJob(Character character, Job job)
    {
        Round.AssignCharacterToJob(character, job);
    }

    //Compute how many of the jobs in the provided jobs list are currently correctly assigned
    private void ComputeCorrect()
    {
        int numCorrectTemp = 0;
        foreach (var job in Dilemma.jobs)
        {
            Character characterTemp;
            if (Round.PickedCharacters.TryGetValue(job, out characterTemp))
            {
                if (job.idealCharacter.Equals(characterTemp))
                {
                    numCorrectTemp++;
                    AddFeedback(job, characterTemp, job.idealAssignedDialogue);
                }
                else
                {
                    AddFeedback(job, characterTemp, characterTemp.incorrectlyAssignedDialogue);
                    AddFeedback(job, job.idealCharacter, job.idealSuggestDialogue);
                }
            }
        }
        Round.NumCorrect = numCorrectTemp;
        if (numCorrectTemp >= Dilemma.minJobSuccess)
        {
            Round.PartiallySucceeded = true;
        }
        if (numCorrectTemp == Dilemma.jobs.Count)
        {
            Round.Succeeded = true;
        }
    }

    //Adds feedback for a specific character
    private void AddFeedback(Job job, Character character, String dialogue)
    {
        Debug.Log($"Added Feedback: {character.firstName} - {dialogue}");
        Dictionary<Character, String> characterFeedback;
        
        if (FeedbackDictionary.TryGetValue(job, out characterFeedback))
        {
            characterFeedback.Add(character, dialogue);
        }
        else
        {
            Debug.Log("Tried adding feedback for a job that isn't in the dictionary!");
        }
    }

    private void OnDestroy() {
        GameManager.onRoundInit -= Play;
    }
}
