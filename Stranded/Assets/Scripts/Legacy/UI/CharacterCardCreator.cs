﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

namespace UI
{
 public class CharacterCardCreator : MonoBehaviour
 {
  public _GameManager gameManager;
  public AssignmentManager assignmentManager;
  public GameObject characterCardPrefab;

  private List<GameObject> cards = new List<GameObject>();
            
  public void CreateCards()
  {
   ClearCards();
   foreach (var character in gameManager.roundManager.characters)
   {
    var characterCard = Instantiate(characterCardPrefab, transform);
    cards.Add(characterCard);
    CharacterCard cCard = characterCard.GetComponent<CharacterCard>();
    cCard.setCharacter(character);
    cCard.assignmentManager = assignmentManager;
    
    var j = gameManager.roundManager.GetJobAssignment(character);
    cCard.setJob(j);
   }
  }

  public void ClearCards()
  {
   foreach (var card in cards)
    {
     Destroy(card);
    }
  }

 }
}