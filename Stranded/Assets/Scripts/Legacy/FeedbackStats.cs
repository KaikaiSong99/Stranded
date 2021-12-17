using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Model;

public class FeedbackStats : MonoBehaviour
{

    // Should be filled in in the future.
    public Text firstName;
    public Image revealedAttribute;
    public Text revealedPoints;
    public Image mood;

    private Character _character;
    
    //TODO change to sprites
    public void Show(Character character)
    {

        string attributeName;

        switch (character.lastRevealed)
        {
            case 0: attributeName = "Creativity"; break;
            case 1: attributeName = "Knowledge";  break;
            case 2: attributeName = "Mental Constitution"; break;
            case 3: attributeName = "Cooperation"; break;
            case 4: attributeName = "Strength"; break; 
            default:  attributeName = "None"; break;
        }

        _character = character;
        firstName.text = String.Format("{0} revealed {1} points in {2} and felt {3}.",
                                        character.firstName, character.attributes[character.lastRevealed], attributeName, character.Mood);
    }


}
