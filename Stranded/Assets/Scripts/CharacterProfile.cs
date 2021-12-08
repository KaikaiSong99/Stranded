using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Model;

public class CharacterProfile : MonoBehaviour
{

    public Image portrait;
    public Text name;
    public Text description;
    public GameObject attributes;

    public Character test;
    private Character _character;
    public Character Character
    { 
        get {
            return _character;
        }
        set 
        {
            _character = value;
            portrait.sprite = _character.portrait;
            name.text = _character.firstName;
            description.text = _character.description; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Character = test;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
