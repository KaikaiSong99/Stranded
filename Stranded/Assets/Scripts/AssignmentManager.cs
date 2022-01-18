using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Model; 

public class AssignmentManager : MonoBehaviour
{

    public Image pickedCharacterPortrait;
    public TextMeshProUGUI jobName;
    public Image jobPortrait;
    public GameObject characterCardRight;
    public GameObject characterCardLeft;
    public Transform container;

    public Sprite emptyCharacterIcon;

    public static event Action<Character, Job> onAssignmentMade;
    private List<GameObject> characterObjects = new List<GameObject>();

    public Job Job 
    { get; set; }
    
    void Start() {
        CharacterScript.onCharacterPressed += MakeAssignment;   
    }

    public void Display(List<Character> characters, Job toPickJob, Round round)
    {
        ClearObjects();
        gameObject.SetActive(true);
        Job = toPickJob;
        int counter = 0;

        Character characterTemp;
        if (round.PickedCharacters.TryGetValue(toPickJob, out characterTemp))
            pickedCharacterPortrait.sprite = characterTemp.portrait;
        else
            pickedCharacterPortrait.sprite = emptyCharacterIcon;

        jobPortrait.sprite = toPickJob.jobIcon;
        jobName.text = toPickJob.jobTitle;

        foreach (var character in characters)
        {
            GameObject go;
            
            if (counter++ % 2 == 0)
                go = Instantiate(characterCardRight, container);
            else 
                go = Instantiate(characterCardLeft, container);

            go.transform.parent = container;
            var prevRectTransform = go.GetComponent<RectTransform>();

            prevRectTransform.sizeDelta = new Vector2(prevRectTransform.sizeDelta.x, 340);

            var charScript = go.GetComponent<CharacterScript>();
            var pickedJob = round.PickedCharacters.FirstOrDefault(e => e.Value == character).Key;
            charScript.Character = character;
            charScript.SetPickedJob(pickedJob);

        }

    }

    private void ClearObjects()
    {
        foreach (var obj in characterObjects)
        {
            Destroy(obj);
        }

        characterObjects.Clear();
    }


    private void MakeAssignment(Character character)
    {
        onAssignmentMade?.Invoke(character, Job);
        gameObject.SetActive(false);
    }



    void OnDestroy() {
        CharacterScript.onCharacterPressed -= MakeAssignment;
    }


}
