using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Model;

public class CharacterCard : MonoBehaviour, IPointerDownHandler
{
    public Text Name;
    public Image Portrait;
    public Image JobIndicator;
    public Image HappinessIndicator;
    public AssignmentManager assignmentManager;

    public Character character
    { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //Name.text = "test";
        //Portrait.sprite = character.portrait;
        //JobIndicator.sprite = job.jobIcon;
    }

    public void OnPointerDown(PointerEventData data)
    {
        assignmentManager.SetCharacterCard(this);
        assignmentManager.ShowOverview(character);
    }

    public void setCharacter(Character c)
    {
        character = c;
        Name.text = c.name;
        Portrait.sprite = c.portrait;
    }

    public void setJob(Job j)
    {
        JobIndicator.sprite = j.jobIcon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Debug.Log(Name.text);
    }
}
