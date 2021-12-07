using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class RoundManager : MonoBehaviour
{
    
    public List<Character> characters;
    public List<Job> jobs;

    public int feedbackDurationInSeconds = 210;
    public int assignmentDurationInSeconds = 90;
    public int maxObtainableScore = 40;

    private float timeLeft;
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
        timeLeft = assignmentDurationInSeconds;
        _round = new Round();
        _assigneds = new Dictionary<Character, Job>();

    }

    public IEnumerator Play()
    {

        Init();

        // TODO Do something with the phases.


        yield return null;
    }

    public IEnumerator PlayAssignmentPhase()
    {
        yield return null;
    }

    public IEnumerator PlayFeedbackPhase()
    {
        _round.Score = CalculateScore();
        AssignMoods();

        // TODO Show feedback in the game

        yield return null;
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

    private void AssignMoods()
    {
        foreach (KeyValuePair<Character, int> score in _round.IndividualScores)
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


    public IEnumerator Timer(int timeInSeconds) 
    {
        while (timeLeft > timeInSeconds) {
            --timeLeft;
            yield return new WaitForSeconds(1f);
        }
    }
}
