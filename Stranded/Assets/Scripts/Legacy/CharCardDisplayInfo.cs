using Model;
using UnityEngine;
using UnityEngine.UI;

public class CharCardDisplayInfo : MonoBehaviour
{
    public Text characterName;

    public Text age;

    public Text description;

    public Image portrait;
    
    public void SetCharacterInfo(Character character)
    {
        age.text = character.age.ToString();
        characterName.text = character.name;
        description.text = character.description;
        portrait.sprite = character.portrait;
    }

    public void OnClickCloseButton()
    {
        gameObject.SetActive(false);
    }
}
