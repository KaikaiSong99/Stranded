using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Legacy
{
    public class CharCard : MonoBehaviour
    {
        public Text characterName;
        public Image portrait;
    
        private Character _character;
    
        [HideInInspector]
        public GameObject characterInfo;

        [HideInInspector]
        public CharacterManager characterManager;
    
        [HideInInspector]
        public GameObject charactersView;
    
        [HideInInspector]
        public JobCard jobCard;
        public Character Character
        {
            get => _character;
            set { 
                _character = value; 
                characterName.text = value.name;
                portrait.sprite = value.portrait;
            }
        }

        public void OnInfoButtonClick()
        {
            characterInfo.SetActive(true);
            var cardInfo = characterInfo.GetComponent<CharCardDisplayInfo>();
            if (cardInfo != null)
            {
                cardInfo.SetCharacterInfo(Character);
            }
        }

        public void CloseInfo()
        {
            characterInfo.SetActive(false);
        }
  
        public void OnCharCardClick()
        { 
            charactersView.SetActive(false);

            characterManager.roundManager.AssignCharacterToJob(Character, jobCard.job);
            //characterManager.roundManager.jobManager.RefreshJobCards();
        }
    }
}
