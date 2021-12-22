using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Model;


public class Chatbox : MonoBehaviour
{
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI monologueText;
    public Image characterPortrait;


    public void Show(Character character, string feedback)
    {
        characterName.text = character.firstName;
        monologueText.text = feedback;
        characterPortrait.sprite = character.portrait;
    }
    

}
