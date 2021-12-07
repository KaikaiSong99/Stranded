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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Play()
    {
        timeLeft = assignmentDurationInSeconds;
        _round = new Round();

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
        int score = 0;
        foreach (Job job in jobs)
        {
            score += job.calculateScore();
        }
        return score;
    }

      // Timer count up
    public IEnumerator Timer(int timeInSeconds) 
    {
        while (timeLeft < timeInSeconds) {
            --timeLeft;
            yield return new WaitForSeconds(1f);
        }
    }
}
