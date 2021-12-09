using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class DataDisplay : MonoBehaviour
{
    public Character c;
    public Job j;
    public int happiness;
    public Text characterName;
    public GameObject characterAge;
    public GameObject characterDescription;
    public GameObject characterPortrait;
    
    public GameObject emotion;
    
    public GameObject creativityIcon;
    public GameObject knowledgeIcon;
    public GameObject mentalConstitutionIcon;
    public GameObject cooperationIcon;
    public GameObject strengthIcon;
    // public Text jobName;
    public GameObject jobName;
    public GameObject jobIcon;
    
    public GameObject characterCreativity;

    public GameObject characterKnowledge;

    public GameObject characterMentalConstitution;

    public GameObject characterCooperation;

    public GameObject characterStrength;
    
    public GameObject jobCreativity;

    public GameObject jobKnowledge;

    public GameObject jobMentalConstitution;

    public GameObject jobCooperation;

    public GameObject jobStrength;

    public Sprite happySprite;
    public Sprite neutralSprite;
    public Sprite sadSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        SetCharacterInfo();
        SetJobInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCharacterInfo()
    {
        characterAge.GetComponent<Text>().text = c.age.ToString();
        characterName.text = c.name;
        characterDescription.GetComponent<Text>().text = c.description;
        characterPortrait.GetComponent<Image>().sprite = c.portrait;
        characterCreativity.GetComponent<Text>().text = c.attributes[0].ToString();
        characterKnowledge.GetComponent<Text>().text = c.attributes[1].ToString();
        characterMentalConstitution.GetComponent<Text>().text = c.attributes[2].ToString();
        characterCooperation.GetComponent<Text>().text = c.attributes[3].ToString();
        characterStrength.GetComponent<Text>().text = c.attributes[4].ToString();
    }

    void SetJobInfo() ///
    {
        jobName.GetComponent<Text>().text = j.name;
        jobIcon.GetComponent<Image>().sprite = j.jobIcon;
        jobCreativity.GetComponent<Text>().text = j.attributes[0].ToString();
        jobKnowledge.GetComponent<Text>().text = j.attributes[1].ToString();
        jobMentalConstitution.GetComponent<Text>().text = j.attributes[2].ToString();
        jobCooperation.GetComponent<Text>().text = j.attributes[3].ToString();
        jobStrength.GetComponent<Text>().text = j.attributes[4].ToString();
    }
    
    //Character c, Dictionary<Character, int> IndividualScores
    void SetEmotion(Character c, Dictionary<Character, int> IndividualScores)
    {
        var pointsGained = IndividualScores[c];
        if (pointsGained<15)
        {
            happiness = 0;
        } else if (pointsGained<25)
        {
            happiness = 1;
        }
        else
        {
            happiness = 2;
        }

        if (happiness == 0)
        {
            emotion.GetComponent<Image>().sprite = happySprite;
        } else if (happiness == 1)
        {
            emotion.GetComponent<Image>().sprite = neutralSprite;
        }
        else
        {
            emotion.GetComponent<Image>().sprite = sadSprite;
        }
    }
}
