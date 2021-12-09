using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public RoundManager roundManager;
    public float timeRemaining; // seconds
    public Text timeText;
    public bool timerCounting = false;

    private void Start()
    {
        
        timerCounting = true;
    }
    // Update is called once per frame
    void Update()
    {
        timeRemaining = roundManager.timeLeft;

        //if(timeremaining > 0)
        //{
        //    timeremaining -= time.deltatime;
        //}
        //else
        //{
        //debug.log("end of round!");
        //   timeremaining = 0;
        //   timercounting = false;
        // call end of this round
        //}
        Displaytime(timeRemaining);


    }
    void Displaytime(float timeToDisplay)
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
