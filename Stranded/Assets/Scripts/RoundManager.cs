using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public Text scoreText;
    public Text roundText;
    public List<Character> characters;
    public List<Job> jobs;

    public int feedbackDurationInSeconds = 210;
    public int assignmentDurationInSeconds = 90;
    public int maxObtainableScore = 40;

    public FeedbackManager feedbackManager;

    public float timeLeft;
    private Round _round;
    public Round Round
    { get; }  

    private Dictionary<Character, Job> _assigneds = new Dictionary<Character, Job>();

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Init()
    {
        feedbackManager.gameObject.SetActive(false);
        _round = new Round();
        _assigneds = new Dictionary<Character, Job>();
    }

    public IEnumerator Play()
    {
        Init();
        yield return StartCoroutine(PlayAssignmentPhase());

        // TODO Do something with the phases.
    }

    public IEnumerator PlayAssignmentPhase()
    {
        timeLeft = assignmentDurationInSeconds;
        yield return StartCoroutine(Timer(() => PlayFeedbackPhase()));
    }

    public IEnumerator PlayFeedbackPhase()
    {
        timeLeft = feedbackDurationInSeconds;
        _round.Score = CalculateScore();
        AssignMoods();
        RevealAttributes();
        feedbackManager.gameObject.SetActive(true);
        yield return StartCoroutine(feedbackManager.ShowFeedback(_round.Score, new List<Character>(_assigneds.Keys)));
    }

    private int CalculateScore()
    {
        int totalScore = 0;

        foreach (KeyValuePair<Character, Job> assigned in _assigneds)
        {
            int characterScore = 0;
            Character character = assigned.Key;
            Job job = assigned.Value; 

            for (int i = 0; i < character.attributes.Length; ++i) 
            {
                characterScore += character.attributes[i] * job.attributes[i];
            }

            _round.IndividualScores.Add(character, characterScore);
            characterScore *= job.importance;
            totalScore += characterScore;
        }
        return totalScore;
    }
    

    public void AddAssignment(Character character, Job job)
    {
        if (_assigneds.ContainsKey(character))
        {
            _assigneds[character] = job;
        }
        else
        {
            _assigneds.Add(character, job);
        }
    }

    private List<int> sortedDescendingAttributeIndices(Job job)
    {
        List<int> sortedIndices = new List<int>();

        for (int i = 0; i < job.attributes.Length; ++i) 
        {
            int highestVal = 0;
            int highestIndex = 0;
            for (int j = 0; j < job.attributes.Length; ++j) 
            {
                if (!sortedIndices.Contains(j) && job.attributes[j] > highestVal)
                {
                    highestVal = job.attributes[j];
                    highestIndex = j;
                }
            }
            sortedIndices.Add(highestIndex);
        }
        return sortedIndices;
    }

    private void RevealAttributes()
    {
        foreach (var assigned in _assigneds)
        {
            Character character  = assigned.Key;
            Job job  = assigned.Value;

            List<int> sortedIndices = sortedDescendingAttributeIndices(job);
            
            foreach (int index in sortedIndices)
            {
                if (character.revealedAttribute[index] == false)
                {
                    character.lastRevealed = index;
                    character.revealedAttribute[index] = true; 
                    break;
                }
            } 

        }
    }

    private void AssignMoods()
    {
        foreach (var score in _round.IndividualScores)
        {
            Character character  = score.Key;
            int individualScore  = score.Value;
            character.Mood = GetMood(individualScore);
        }
    }


    private Mood GetMood(int score) 
    {
        Mood mood = Mood.Neutral;

        if (score < maxObtainableScore * 0.4)
        {
            mood = Mood.Sad;
        }
        else if (score > maxObtainableScore * 0.6 ) 
        {
            mood = Mood.Happy;
        }

        return mood;
    }


      // Timer count up
    public IEnumerator Timer(Func<IEnumerator> func) 
    {
        while (timeLeft > 0) {
            --timeLeft;
            yield return new WaitForSeconds(1f);
        }

        yield return func();
    }

    void DisplayScoreRound(int score, int round)// now only score
    {
        scoreText.text = "Score: " + score.ToString("0000");
        roundText.text = "Round: " + round.ToString("00");

    }

    public Job getAssignment(Character character)
    {
        Debug.Log(_assigneds);
        Job res;
        _assigneds.TryGetValue(character, out res);
        return res;
    }
}
