using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class CharCardDisplayInfo : MonoBehaviour
{
    public Text name;

    public Text age;

    public Button button;
    //public Text gender;

    public Text description;

    public Image portrait;
    // Start is called before the first frame update
    public void SetCharacterInfo(Character character)
    {
        age.text = character.age.ToString();
        name.text = character.name;
        description.text = character.description;
        //gender.text = character.gender;
        portrait.sprite = character.portrait;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
