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
    // [HideInInspector]
    public List<Transform> containers;
    private List<GameObject> objects = new List<GameObject>();
    public JobCard jobCard;
    public RoundManager roundManager;
    public GameObject infoUI;
    
    public void Start()
    {
     
    }
 
    public void CreateCards(Dilemma dilemma) 
    {
       
        ClearCards();
        Debug.Log(dilemma.characters.Count);
        for (int i=0; i<dilemma.characters.Count; i++)
        {
            var character = dilemma.characters[i];
           
            var charCard = Instantiate(charCardPrefab, containers[i].transform);
            
            objects.Add(charCard);
            charCard.transform.parent = containers[i];
            
            var cCard = charCard.GetComponent<CharCard>();
            cCard.infoUI = infoUI;
            cCard.characterManager = this;
            cCard.character = character;
            cCard.charactersView = gameObject;
            cCard.jobCard = jobCard;
            cCard.characterManager.roundManager = roundManager;
        }
    }

    private void ClearCards()
    {
        foreach (var card in objects)
        {
            Destroy(card);
        }
    }
}
