using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{    

    [CreateAssetMenu(fileName = "newCharacter", menuName = "Stranded/Character", order = 0)]
    public class Character : ScriptableObject, IEquatable<Character>
    {
        
        // TODO other personality types when useful.
        public enum Personality {
            QUIRKY,
            HASTY,
            QUICK_WITTED,
            IMPULSIVE,
            EXTROVERTED,
            INTROVERTED
        }

        public new string name;
        public int age;
        public string description;
        public Personality personality;

        public int[] attributes;

        // id is used to index the character by the game manager
        [SerializeField]
        private int _id = -1;
        public int Id
        { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Character);
        }

        public override int GetHashCode() {
            return _id;
        }

        public bool Equals(Character other) 
        {
            if (other == null)
                return false;
            return _id == other._id;
        }
        

    }
}