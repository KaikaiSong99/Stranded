using UnityEngine;

namespace UI
{
  public class CharacterCardCreator : MonoBehaviour
  {
    public GameManager gameManager;

    public GameObject characterCardPrefab;
    private void Awake()
    {
      foreach (var character in gameManager.roundManager.characters)
      {
        var characterCard = Instantiate(characterCardPrefab, transform);
        characterCard.name += $" {character.name}";
      }
    }
  }
}