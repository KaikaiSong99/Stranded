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


        [SerializeField]
        private int _id = -1;
        public int Id
        { get; set; }

      

    }
}
