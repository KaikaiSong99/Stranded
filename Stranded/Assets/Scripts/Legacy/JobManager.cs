using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Legacy
{
    public class JobManager : MonoBehaviour
    {
        public GameObject jobCardPrefab;
        public List<Transform> containers;
        private List<GameObject> objects = new List<GameObject>();
        public GameObject characterOverview;
        public CharacterManager characterManager;
        public RoundManager roundManager;

        public void CreateCards(Dilemma dilemma) //create Job cards
        {
            ClearCards();
            for (int i=0; i<dilemma.jobs.Count; i++)
            {
                Job job = dilemma.jobs[i];
                GameObject jobCard = Instantiate(jobCardPrefab, containers[i]);
                objects.Add(jobCard);
                JobCard jCard = jobCard.GetComponent<JobCard>();
                jCard.characterManager = characterManager;
                jCard.characterOverview = characterOverview;
                jCard.dilemma = dilemma;
            
                jCard.roundManager = roundManager;
                jCard.job = job;
            }
        }

        public void RefreshJobCards()
        {
        
            foreach (GameObject jobCard in objects)
            {
                JobCard jCard = jobCard.GetComponent<JobCard>();
                jCard.characterOverview = characterOverview;
                Character characterTemp;
                if (roundManager.round.PickedCharacters.TryGetValue(jCard.job, out characterTemp))
                {
                    jCard.characterName.text = characterTemp.name;
                    jCard.portrait.sprite = characterTemp.portrait;
                }
                else
                {
                    jCard.characterName.text = "????";
                    jCard.portrait.sprite = null;
                }
       
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
}