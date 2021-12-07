using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model 
{
    [CreateAssetMenu(fileName = "newJob", menuName = "Stranded/Job", order = 0)]
    public class Job : ScriptableObject
    {

        public new string name;
        public string description;
        public int maxAssignments;
        public int importance;

        public int[] attributes;


        private List<Character> _assignees = new List<Character>();
        public List<Character> Assignees
        {
            get;
        }


        public int calculateScore()
        {
            int score = 0;
    
            foreach (Character character in _assignees)
            {
                for (int i = 0; i < attributes.Length; ++i) 
                {
                    score += character.attributes[i] * attributes[i];
                }
                score *= importance;
            }

            return score;
        }

        public bool Assign(Character character) 
        {
            if (_assignees.Count > 2) {
                return false;
            }

            _assignees.Add(character);
            return true;
        }

        public bool Remove(Character character)
        {
            return _assignees.Remove(character);
        }

        public bool RemoveAt(int index) {
            if (index < 0 || index >= maxAssignments) {
                return false;
            }

            _assignees.RemoveAt(index);
            return true;
        }

        public void Clear() {
            _assignees.Clear();
        }

    }
}
