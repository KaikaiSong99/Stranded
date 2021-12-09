using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model 
{

    // Contains statistics for the current round.
    public class Round : MonoBehaviour
    {
        private int _score;
        private int Score
        { get; set;}

        public Dictionary<Character, int> IndividualScores
        {
            get;
            set;
        }

        private List<Character> _orderAssigned;
        public List<Character> OrderAssigned
        { get; set;}

        public Round()
        {
            
        }

    }

}
