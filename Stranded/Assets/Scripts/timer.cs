using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float timeRemaining = 65; // seconds
    public Text timeText;
    public bool timerCounting = false;

    private void Start()
    {
        timerCounting = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            //Debug.Log("End of round!");
            timeRemaining = 0;
            timerCounting = false;
            // call end of this round
        }
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
