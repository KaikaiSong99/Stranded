using UnityEngine;
using Model;

namespace UI
{
 public class CharacterCardCreator : MonoBehaviour
 {
  public GameManager gameManager;
  public GameObject characterCardPrefab;
            
  public void Start()
  {
   foreach (var character in gameManager.roundManager.characters)
   {
    var characterCard = Instantiate(characterCardPrefab, transform);
    CharacterCard cCard = characterCard.GetComponent<CharacterCard>();
    cCard.setCharacter(character);
    Debug.Log("Test");
    Job j = gameManager.roundManager.getAssignment(character);
    if (j != null)
    {
     cCard.setJob(j);
    }
   }
  }
 }
}