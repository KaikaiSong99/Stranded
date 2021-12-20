using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.UI;
using UI;

public class _RoundManager : MonoBehaviour
{
    public List<Character> characters;
    public List<Job> jobs;

    public int feedbackDurationInSeconds = 210;
    public int assignmentDurationInSeconds = 90;
    public int maxObtainableScore = 40;
    private int totalScore = 0;

    public FeedbackManager feedbackManager;
    public TopBarManager topBarManager;
    public CharacterCardCreator cardCreator;
    public GridLayoutGroupAutoScaler gridScaler;

    public float timeLeft;

    private Dictionary<Character, Job> _assigneds = new Dictionary<Character, Job>();

    // Start is called before the first frame update
    void Start()
    {
 
    }

    private void Init()
    {
        topBarManager.DisplayScore(totalScore);
        feedbackManager.gameObject.SetActive(false);
        _assigneds = new Dictionary<Character, Job>();

        foreach (Character c in characters)
        {
            AddAssignment(c, jobs[0]);
        }
        
        cardCreator.CreateCards();
        gridScaler.SetLayout();
        
        
        // TODO change the job icon for unassigned character to be idle
    }

    public IEnumerator Play(int currentRound)
    {
        topBarManager.DisplayRound(currentRound);
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
        AssignMoods();
        RevealAttributes();
        feedbackManager.gameObject.SetActive(true);
        yield return StartCoroutine(feedbackManager.ShowFeedback(0, new List<Character>(_assigneds.Keys)));
    }

    private int CalculateScore()
    {
        int totalScore = 0;

        // foreach (KeyValuePair<Character, Job> assigned in _assigneds)
        // {
        //     int characterScore = 0;
        //     Character character = assigned.Key;
        //     Job job = assigned.Value; 

        //     for (int i = 0; i < character.attributes.Length; ++i) 
        //     {
        //         characterScore += character.attributes[i] * job.attributes[i];
        //     }

        //     characterScore *= job.importance;
        //     totalScore += characterScore;
        // }
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

    //     for (int i = 0; i < job.attributes.Length; ++i) 
    //     {
    //         int highestVal = 0;
    //         int highestIndex = 0;
    //         for (int j = 0; j < job.attributes.Length; ++j) 
    //         {
    //             if (!sortedIndices.Contains(j) && job.attributes[j] > highestVal)
    //             {
    //                 highestVal = job.attributes[j];
    //                 highestIndex = j;
    //             }
    //         }
    //         sortedIndices.Add(highestIndex);
    //     }
        return sortedIndices;
    }

    private void RevealAttributes()
    {
        // foreach (var assigned in _assigneds)
        // {
        //     Character character  = assigned.Key;
        //     Job job  = assigned.Value;

        //     List<int> sortedIndices = sortedDescendingAttributeIndices(job);
            
        //     foreach (int index in sortedIndices)
        //     {
        //         if (character.revealedAttribute[index] == false)
        //         {
        //             // character.lastRevealed = index;
        //             // character.revealedAttribute[index] = true; 
        //             break;
        //         }
        //     } 

        // }
    }

    private void AssignMoods()
    {

    }


    // private Mood GetMood(int score) 
    // {
    //     Mood mood = Mood.Neutral;

    //     if (score < maxObtainableScore * 0.4)
    //     {
    //         mood = Mood.Sad;
    //     }
    //     else if (score > maxObtainableScore * 0.6 ) 
    //     {
    //         mood = Mood.Happy;
    //     }

    //     return mood;
    // }


      // Timer count up
    public IEnumerator Timer(Func<IEnumerator> func) 
    {
        while (timeLeft > 0) {
            topBarManager.Displaytime(timeLeft);
            --timeLeft;
            yield return new WaitForSeconds(1f);
        }

        yield return func();
    }

    public Job GetJobAssignment(Character character)
    {
        if (!_assigneds.TryGetValue(character, out var j))
        {
            Debug.LogWarning($"Character {character.name} is assigned to a null job. Assigned default job " +
                             $"of {jobs[0].name}.");
        }
        return j != null ? j : jobs[0];
    }


    // Assign Character c to Job j
    // Return the Job the character was unassigned from, return null if the character was not assigned to any job yet
    public Job AssignCharacter(Character c, Job j)
    {
        Job res = null;
        if (_assigneds.ContainsValue(j))
        {
            if (_assigneds.TryGetValue(c, out res) && res.Equals(j))
            {
                return j;
            } 
            else
            {
                ClearJob(j);
            }
        }

        if (_assigneds.ContainsKey(c))
        {
            _assigneds.TryGetValue(c, out res);
            _assigneds.Add(c, j);
        }
        
        return res;
    }

    // Removes any key value pairs from _assigneds where the Job equals j
    private void ClearJob(Job j)
    {
        foreach (KeyValuePair<Character, Job> assigned in _assigneds)
        {
            if (assigned.Value.Equals(j))
            {
                _assigneds.Remove(assigned.Key);
            }
        }
    }
}
