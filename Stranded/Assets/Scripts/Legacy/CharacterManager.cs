using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Legacy
{
    public class CharacterManager : MonoBehaviour
    {
        public Text jobName;
        public Image icon;
        public GameObject charCardPrefab;
        public List<Transform> containers;
        private readonly List<GameObject> _charCards = new List<GameObject>();
        public RoundManager roundManager;
        public GameObject characterInfo;
    
        [HideInInspector]
        public JobCard jobCard;
    
        public void CreateCards(Dilemma dilemma) 
        {
            ClearCards();
            for (var i = 0; i < dilemma.characters.Count; i++)
            {
                var character = dilemma.characters[i];
           
                var charCard = Instantiate(charCardPrefab, containers[i].transform);
            
                _charCards.Add(charCard);
            
                var cCard = charCard.GetComponent<CharCard>();
                cCard.characterInfo = characterInfo;
                cCard.characterManager = this;
                cCard.Character = character;
                cCard.charactersView = gameObject;
                cCard.jobCard = jobCard;
                cCard.characterManager.roundManager = roundManager;
            }
        }

        private void ClearCards()
        {
            foreach (var card in _charCards)
            {
                Destroy(card);
            }
        }
    }
}
