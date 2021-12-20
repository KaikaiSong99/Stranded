using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class RoundManager : MonoBehaviour
{
    // When the round is done call onRoundEnd?.Invoke(Round round)
    public static event Action<Round> onRoundEnd;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onRoundInit += Play;
    }

    public void Update()
    {

    }


    // Start this round (is called from GameManager)
    // Cast the parameters to Dilemma type with "Dilemma s = parameters as Dilemma"
    // Could also check if it is a Dilemma and return if not
    public void Play(BaseSceneParameter parameters)
    {
        var dilemma = parameters as Dilemma;
        Round round = new Round(dilemma.isCounted, 0);
        onRoundEnd?.Invoke(round);
    }

    void OnDestroy() {
        GameManager.onRoundInit -= Play;
    }
}
