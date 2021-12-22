using System.Collections.Generic;

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
            foreach (KeyValuePair<Job, Character> picked in PickedCharacters)
            {
                if (picked.Value.Equals(character))
                {
                    PickedCharacters.Remove(picked.Key);
                }
            }
        }
    }
}
