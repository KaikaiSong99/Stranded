
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class DataDisplay : MonoBehaviour
{
    public int happiness;
    public Text characterName;
    public Text characterAge;
    public Text characterDescription;
    public Image characterPortrait;
    
    public Image emotion;
    
    public Image creativityIcon;
    public Image knowledgeIcon;
    public Image mentalConstitutionIcon;
    public Image cooperationIcon;
    public Image strengthIcon;
    // public Text jobName;
    public Text jobName;
    public Image jobIcon;
    
    public Text characterCreativity;
    public Text characterKnowledge;
    public Text characterMentalConstitution;
    public Text characterCooperation;
    public Text characterStrength;
    
    public Text jobCreativity;
    public Text jobKnowledge;
    public Text jobMentalConstitution;
    public Text jobCooperation;
    public Text jobStrength;

    public Sprite happySprite;
    public Sprite neutralSprite;
    public Sprite sadSprite;
    
    public void SetCharacterInfo(Character character)
    {
        characterAge.text = character.age.ToString();
        characterName.text = character.name;
        characterDescription.text = character.description;
        characterPortrait.sprite = character.portrait;

        // characterCreativity.text = character.attributes[0].ToString();
        // characterKnowledge.text = character.attributes[1].ToString();
        // characterMentalConstitution.text = character.attributes[2].ToString();
        // characterCooperation.text = character.attributes[3].ToString();
        // characterStrength.text = character.attributes[4].ToString();
    }

    public void SetJobInfo(Job job)
    {
        jobName.text = job.name;
        jobIcon.sprite = job.jobIcon;

        // jobCreativity.text = job.attributes[0].ToString();
        // jobKnowledge.text = job.attributes[1].ToString();
        // jobMentalConstitution.text = job.attributes[2].ToString();
        // jobCooperation.text = job.attributes[3].ToString();
        // jobStrength.text = job.attributes[4].ToString();
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
