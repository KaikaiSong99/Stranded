using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarManager : MonoBehaviour
{

    public float timeRemaining; // seconds
    public Text roundText;
    public Text scoreText;
    public Text timeText;
    public bool timerCounting = false;

    public void DisplayScore(int score) 
    {
        scoreText.text = $"Score: {score}";
    }

    public void DisplayRound(int round)
    {
        roundText.text = $"Round: {round}";
    }


    public void Displaytime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
