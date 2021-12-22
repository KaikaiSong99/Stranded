using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
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
    public Dictionary<Character, List<String>> feedback 
    { get; private set; }

    // Different timers for the different phases
    public float introTime = 5;
    public float executionTime = 5;
    public float feedbackTime = 30;

    
    public float timeLeft;
    // { get; private set; }

    public Text dilemmaTitle;
    public Image dilemmaSprite;

    public FeedbackManager feedbackManager;
    public Canvas feedbackScreen;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onRoundInit += Play;
        feedback = new Dictionary<Character, List<string>>();
    }

    public void ShowOverview()
    {
        dilemmaTitle.text = dilemma.title;
        dilemmaSprite.sprite = dilemma.sprite;
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
        round = new Round(dilemma.isCounted, 0);
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
        Debug.Log("Assignment has started");
        timeLeft = dilemma.playTime;
        foreach (var character in dilemma.characters)
        {
            Debug.Log($"{character.GetId()}");
        }
        AssignCharacterToJob(dilemma.characters[0], dilemma.jobs[0]);
        AssignCharacterToJob(dilemma.characters[1], dilemma.jobs[1]);
        AssignCharacterToJob(dilemma.characters[2], dilemma.jobs[2]);
        AssignCharacterToJob(dilemma.characters[3], dilemma.jobs[3]);
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
        StartCoroutine(feedbackManager.Show(feedback));
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
                    AddFeedback(characterTemp, job.idealAssignedDialogue);
                }
                else
                {
                    AddFeedback(characterTemp, characterTemp.incorrectlyAssignedDialogue);
                }
            }
            AddFeedback(job.idealCharacter, job.idealSuggestDialogue);
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
    private void AddFeedback(Character character, String dialogue)
    {
        Debug.Log($"Added Feedback: {character.firstName} - {dialogue}");
        List<String> characterFeedback;

        if (feedback.TryGetValue(character, out characterFeedback))
        {
            characterFeedback.Add(dialogue);
        }
        else {
            characterFeedback = new List<String> { dialogue };     
            feedback.Add(character, characterFeedback);
        }

    }

    void OnDestroy() {
        GameManager.onRoundInit -= Play;
    }
}
