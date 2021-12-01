using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model 
{
    [System.Serializable]
    public class Attribute {
        // TODO Extend to proper attributes
        public enum AttributeType
        {
            Strength,
            Dexterity,
            Intuition,
            Charisma
        }


        public AttributeType type;
        public int points;
    }
}
