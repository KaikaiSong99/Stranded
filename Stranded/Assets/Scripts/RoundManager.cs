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
        yield return null;
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


      // Timer count up
    public IEnumerator Timer(int timeInSeconds) 
    {
        while (timeLeft < timeInSeconds) {
            --timeLeft;
            yield return new WaitForSeconds(1f);
        }
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
            res = _assigneds.TryGetValue(c);
            _assigneds.Add(c, j);
        }
        
        return res;
    }

    // Removes any key value pairs from _assigneds where the Job equals j
    private void ClearJob(Job j)
    {
        foreach (KeyValuePair<Character, Job> assigned in _assigneds)
        {
            if (assigned.Value.equals(j))
            {
                _assigneds.Remove(assigned.Key);
            }
        }
    }
}
