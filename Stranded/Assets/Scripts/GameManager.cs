using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

// The Game manager is responsible for initiating the rounds
// This includes the introduction and ending.
public class GameManager : MonoBehaviour    
{

    public int numberOfRounds = 5;
    public RoundManager roundManager;
    
    [SerializeField]
    private Round[] roundData;
    private int currentScore = 0;
    private int currentRound = 0;

    // Start is called before the first frame update
    void Start()
    {


        roundData = new Round[numberOfRounds];
    }

    public void NextRound()
    {
        currentRound++;
    }

    private IEnumerator StartGame()
    {
        for (int i = 0; i < numberOfRounds; ++i)
        {
            yield return StartCoroutine(roundManager.Play());
        }
    }


  
}
