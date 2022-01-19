using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model 
{

    // Contains statistics for the current round.
    // TODO: Extend
    public class Round
    {
        public Round(bool isCounted, int numCorrect, Dilemma dilemma)
        {
            IsCounted = isCounted;
            NumCorrect = numCorrect;
            PickedCharacters = new Dictionary<Job, Character>();
            PartiallySucceeded = false;
            Succeeded = false;
            Dilemma = dilemma;
        }

        public bool IsCounted
        { get; set; }

        public bool PartiallySucceeded
        { get; set; }

        public bool Succeeded
        { get; set; }

        public Dictionary<Job, Character> PickedCharacters
        { get; set; }

        public int NumCorrect
        { get; set; }

        public Dilemma Dilemma { get; private set; }

        public bool IsJobCorrectlyAssigned(Job job)
        {
            if (PickedCharacters.TryGetValue(job, out var picked))
            {
                return picked.Equals(job.idealCharacter);
            }
            Debug.LogWarning("Tried checking job correctness before assignment was made.");
            return false;
        }
        
        public void AssignCharacterToJob(Character character, Job job)
        {
            ClearCharacter(character);
            
            if (PickedCharacters.ContainsKey(job))
            {
                PickedCharacters[job] = character;
            }
            else
            {
                PickedCharacters.Add(job, character);
            }

        }

        private void ClearCharacter(Character character)
        {
            Job job = null;
            foreach (KeyValuePair<Job, Character> picked in PickedCharacters)
            {
                if (picked.Value.Equals(character))
                {
                    job = picked.Key;
                    break;
                }
            }
            if (job != null) 
            {
                PickedCharacters.Remove(job);
            }
        }
    }
}
