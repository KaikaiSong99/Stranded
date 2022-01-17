using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Legacy
{
    public class Chatbox : MonoBehaviour
    {
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI monologueText;
        public Image characterPortrait;


        public void Show(Character character, string feedback)
        {
            characterName.text = character.firstName;
            monologueText.text = feedback;
            characterPortrait.sprite = character.portrait;
        }
    

    }
}
