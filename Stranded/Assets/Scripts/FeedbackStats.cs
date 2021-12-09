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
    

    // Start is called before the first frame update

    public void Show(Character character)
    {
        _character = character;
        firstName.text = String.Format("{0} revealed {1} points in {2} and felt {3}.",
                                        character.firstName, 2, "Strength", character.Mood);
    }


}
