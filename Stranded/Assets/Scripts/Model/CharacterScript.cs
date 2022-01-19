using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Model; 

public class CharacterScript : MonoBehaviour, IPointerClickHandler
{
    public Image portrait;
    public TextMeshProUGUI name;
    public TextMeshProUGUI age;
    public TextMeshProUGUI description;
    public Image assignedJob;

    public Character _character;
    public Character Character
    {
        get => _character;
        set { 
            _character = value; 
            name.text = value.firstName;
            portrait.sprite = value.portrait;
            age.text = value.age.ToString();
            description.text = value.description;
        }
    }

    public static event Action<Character> onCharacterPressed;

    public void SetPickedJob(Job job)
    {
        if (job)
        {
            assignedJob.sprite = job.jobIcon;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        onCharacterPressed?.Invoke(_character);
    }




}
