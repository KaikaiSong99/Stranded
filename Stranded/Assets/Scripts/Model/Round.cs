using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model 
{

    // Contains statistics for the current round.
    public class Round
    {
        public int Score
        { get; set;}

        public Dictionary<Character, int> IndividualScores
        { get; set;}

        public List<Character> OrderAssigned
        { get; set;}

        public Round() {
            IndividualScores = new Dictionary<Character, int>();
            OrderAssigned = new List<Character>();    
        }
        

    }

}
