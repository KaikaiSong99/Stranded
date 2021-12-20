using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    // When the round is done call onRoundEnd?.Invoke(Round round)
    public static event Action<Round> onRoundEnd;
    public Text dilemmaTitle;
    
    public Image dilemmaSprite;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onRoundInit += Play;
       
    }
    public void ShowOverview(Dilemma dilemma)
    {
      
        dilemmaTitle.text = dilemma.title;
        dilemmaSprite.sprite = dilemma.sprite;

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
        ShowOverview(dilemma);
    }

    void OnDestroy() {
        GameManager.onRoundInit -= Play;
    }
}
