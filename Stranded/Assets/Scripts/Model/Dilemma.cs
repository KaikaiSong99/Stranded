using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Model 
{
    [CreateAssetMenu(fileName = "newDilemma", menuName = "Stranded/Dilemma", order = 0)]
    public class Dilemma : BaseSceneParameter
    {
        public List<Job> jobs;
        public Sprite sprite;

        public string title;
        [TextArea(1,10)]
        public string description;
        public float playTime;

        [Tooltip("Sets whether the dilemma is counted towards getting an ending.")]
        public bool isCounted;
        public int minJobSuccess;
        [TextArea(1,10)]
        public string successText;
        [TextArea(1,10)]
        public string partialSuccessText;
        [TextArea(1,10)]
        public string failureText;

        private readonly int _id = SimpleIdGenerator.NextId;

        public override bool Equals(object other)
        {
            if (other is Dilemma dilemma)
            {
                return _id == dilemma._id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _id;
        }
    }
}
