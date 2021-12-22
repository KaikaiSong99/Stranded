using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    //public CanvasGroup assignment;
    private CharCard _currentCharCard;
    private Character _currentCharacter;
    public Text jobName;
    public Image icon;
    public GameObject charCardPrefab;
    [HideInInspector]
    public List<Transform> containers;
    private List<GameObject> objects = new List<GameObject>();
 

    
    public void Start()
    {
     
    }
    public void OnAssignClicked(){
        
        gameObject.SetActive(false);
    }
    public void CreateCards(Dilemma dilemma) 
    {
       
        ClearCards();
        Debug.Log(dilemma.characters.Count);
        for (int i=0; i<dilemma.characters.Count; i++)
        {
            Character character = dilemma.characters[i];
           
            GameObject charCard = Instantiate(charCardPrefab, containers[i].transform);
            objects.Add(charCard);
            charCard.transform.parent = containers[i];
            
            CharCard cCard = charCard.GetComponent<CharCard>();
            cCard.character = character;
        }
    }

    public void ClearCards()
    {
        foreach (var card in objects)
        {
            Destroy(card);
        }
    }
}
