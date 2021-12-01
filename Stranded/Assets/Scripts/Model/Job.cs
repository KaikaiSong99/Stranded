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

        public Attribute[] proficiency;


        private List<GameObject> _assignees = new List<GameObject>();
        public List<GameObject> Assignees
        {
            get;
        }

        //TODO More attributes of a job

        public bool Assign(GameObject character) 
        {
            if (_assignees.Count > 2) {
                return false;
            }

            _assignees.Add(character);
            return true;
        }

        public bool Remove(GameObject character)
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
