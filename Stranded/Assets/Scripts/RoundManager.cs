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

    public float timeLeft;
    private Round _round;
    public Round Round
    { get; }  

    private Dictionary<Character, Job> _assigneds; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Init()
    {
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
        yield return StartCoroutine(Timer(() => null));
    }

    public int CalculateScore()
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
}
