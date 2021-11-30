using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model 
{
    public class Job : MonoBehaviour
    {

        // Editor Variables
        public string m_name;
        public string m_description;
        public int m_maxAssignments;
        public List<Attribute> proficiencies;

        private List<GameObject> m_assignees;

        //TODO More attributes of a job

        // Start is called before the first frame update
        void Start()
        {
            m_assignees = new List<GameObject>();
            Debug.Log("Job name: " + m_name);
            Debug.Log("Short Job description: " + m_description);
        }

        public bool Assign(GameObject character) 
        {
            if (m_assignees.Count > 2) {
                return false;
            }

            m_assignees.Add(character);
            return true;
        }

        public bool Remove(GameObject character)
        {
            return m_assignees.Remove(character);
        }

        public bool RemoveAt(int index) {
            if (index < 0 || index >= m_maxAssignments) {
                return false;
            }

            m_assignees.RemoveAt(index);
            return true;
        }

        public void Clear() {
            m_assignees.Clear();
        }

    }
}
