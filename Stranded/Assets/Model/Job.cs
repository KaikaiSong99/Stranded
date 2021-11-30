using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model 
{
    public class Job : MonoBehaviour
    {

        public string m_name;
        public string m_description;
        public List<Attribute> proficiencies;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Job name: " + m_name);
            Debug.Log("Short Job description: " + m_description);
        }
    }
}
