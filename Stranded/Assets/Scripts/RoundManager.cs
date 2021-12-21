using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using TMPro;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    // When the round is done call onRoundEnd?.Invoke(Round round)
    public static event Action<Round> onRoundEnd;
    public Button assignButton;
    public TextMeshProUGUI b1text;
    public Dilemma dilemma;
    public Text dilemmaTitle;
    
    public Image dilemmaSprite;

    public Text dilemmaDescription;

    public Text jobName1, jobName2, jobName3;

    public Image characterSprite1, characterSprite2, characterSprite3;
    // Start is called before the first frame update
    void Start()
    {   
        b1text = assignButton.GetComponentInChildren<TextMeshProUGUI>();
        btnValue();
        ShowOverview(dilemma);
        GameManager.onRoundInit += Play;
        
    }

    public void btnValue()
    {
        b1text.text = "Assign";
    }
    
    public void ShowOverview(Dilemma dilemma)
    {
      
        dilemmaTitle.text = dilemma.title;
        dilemmaSprite.sprite = dilemma.sprite;
        dilemmaDescription.text = dilemma.description;
        jobName1.text = dilemma.jobs[0].ToString();
        jobName2.text = dilemma.jobs[1].ToString();
        jobName3.text = dilemma.jobs[2].ToString();

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
        // ShowOverview(dilemma);
    }

    void OnDestroy() {
        GameManager.onRoundInit -= Play;
    }
}
