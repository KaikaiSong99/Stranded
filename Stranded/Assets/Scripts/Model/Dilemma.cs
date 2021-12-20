using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model 
{
    [CreateAssetMenu(fileName = "newDilemma", menuName = "Stranded/Dilemma", order = 0)]
    public class Dilemma : BaseSceneParameter
    {
        public List<Job> jobs;
        public Sprite sprite;

        [HideInInspector]
        public List<Character> characters;

        public string title;
        [TextArea(1,10)]
        public string description;
        public float playTime;

        public bool isCounted;
        public int minJobSuccess;
        public string successText;
        public string partialSuccessText;
        public string failureText;        
    }
}
