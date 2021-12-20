using System.Collections.Generic;

namespace Model 
{

    // Contains statistics for the current round.
    public class Round
    {
        public Round(bool isCounted, int numCorrect)
        {
            IsCounted = isCounted;
            NumCorrect = numCorrect;
            PickedCharacters = new Dictionary<Job, Character>(); 
        }

        public bool IsCounted
        { get; set; }

        public Dictionary<Job, Character> PickedCharacters
        { get; set; }

        public int NumCorrect
        { get; set; }

    }
}
