using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    // When the round is done call onRoundEnd?.Invoke(Round round)
    public static event Action<Round> onRoundEnd;

    public Dilemma dilemma
    { get; private set; }
    public Round round
    { get; private set; }

    //Dictionary containing feedback dialogue per characters, since there is a case where a single character has more than one piece of feedback, the dialogue is stored in a list.
    public Dictionary<Job, Dictionary<Character,String>> feedback 
    { get; private set; }

    // Different timers for the different phases
    public float introTime = 5;
    public float executionTime = 5;
    public float feedbackTime = 30;
    public JobManager jobManager;
  
    public float timeLeft;
    // { get; private set; }

    public Text dilemmaTitle;
    public Image dilemmaSprite;
    public CanvasGroup overviewUI;
    public CanvasGroup jobOverviewUI;

    public FeedbackManager feedbackManager;
    public Canvas feedbackScreen;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onRoundInit += Play;
    }

    public void ShowOverview()
    {
        dilemmaTitle.text = dilemma.title;
        dilemmaSprite.sprite = dilemma.sprite;
    }

    public void ShowAssignmentOverview()
    {
        overviewUI.gameObject.SetActive(false);
        jobOverviewUI.gameObject.SetActive(true);
        jobManager.roundManager = this;
        jobManager.CreateCards(dilemma);

        Debug.Log($" Job count: {dilemma.jobs.Count}");
    }
    
    public void Update()
    {
    }

    // Start this round (is called from GameManager)
    // Cast the parameters to Dilemma type with "Dilemma s = parameters as Dilemma"
    // Could also check if it is a Dilemma and return if not
    public void Play(BaseSceneParameter parameters)
    {
        dilemma = parameters as Dilemma;
        
        round = new Round(dilemma.isCounted, 0, dilemma);
        feedback = new Dictionary<Job, Dictionary<Character,String>>();
        foreach (var job in dilemma.jobs)
        {
            feedback.Add(job, new Dictionary<Character, String>());
        }
        StartCoroutine(PlayIntroPhase());
    }

    public IEnumerator PlayIntroPhase()
    {
        Debug.Log("Intro has started");
        timeLeft = introTime;
        ShowOverview();
        yield return StartCoroutine(Timer(() => PlayAssignmentPhase()));
    }

    public IEnumerator PlayAssignmentPhase()
    {
        ShowAssignmentOverview();
        Debug.Log("Assignment has started");
        timeLeft = dilemma.playTime;
        yield return StartCoroutine(Timer(() => PlayExecutionPhase()));
    }

    public IEnumerator PlayExecutionPhase()
    {
        Debug.Log("Execution has started");
        timeLeft = executionTime;
        ComputeCorrect();
        yield return StartCoroutine(Timer(() => PlayFeedbackPhase()));
    }

    public IEnumerator PlayFeedbackPhase()
    {
        Debug.Log("Feedback has started");
        timeLeft = feedbackTime;
        feedbackScreen.gameObject.SetActive(true);
        StartCoroutine(feedbackManager.Show(feedback, round.PickedCharacters));
        yield return StartCoroutine(Timer(() => null));
        onRoundEnd?.Invoke(round);
    }

    public IEnumerator Timer(Func<IEnumerator> func)
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
        round.AssignCharacterToJob(character, job);
    }

    //Compute how many of the jobs in the provided jobs list are currently correctly assigned
    private void ComputeCorrect()
    {
        int numCorrectTemp = 0;
        foreach (var job in dilemma.jobs)
        {
            Character characterTemp;
            if (round.PickedCharacters.TryGetValue(job, out characterTemp))
            {
                if (job.idealCharacter.Equals(characterTemp))
                {
                    numCorrectTemp++;
                    AddFeedback(job, characterTemp, job.idealAssignedDialogue);
                }
                else
                {
                    AddFeedback(job, characterTemp, characterTemp.incorrectlyAssignedDialogue);
                }
            }
            AddFeedback(job, job.idealCharacter, job.idealSuggestDialogue);
        }
        round.NumCorrect = numCorrectTemp;
        if (numCorrectTemp >= dilemma.minJobSuccess)
        {
            round.PartiallySucceeded = true;
        }
        if (numCorrectTemp == dilemma.jobs.Count)
        {
            round.Succeeded = true;
        }
    }

    //Adds feedback for a specific character
    private void AddFeedback(Job job, Character character, String dialogue)
    {
        Debug.Log($"Added Feedback: {character.firstName} - {dialogue}");
        Dictionary<Character, String> characterFeedback;
        
        if (feedback.TryGetValue(job, out characterFeedback))
        {
            characterFeedback.Add(character, dialogue);
        }
        else
        {
            Debug.Log("Tried adding feedback for a job that isn't in the dictionary!");
        }

    }

    void OnDestroy() {
        GameManager.onRoundInit -= Play;
    }
}
