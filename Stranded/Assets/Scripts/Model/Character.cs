using System;
using UnityEngine;

namespace Model
{    
        // TODO other personality types when useful.
    public enum Personality 
    {
        QUIRKY,
        HASTY,
        QUICK_WITTED,
        IMPULSIVE,
        EXTROVERTED,
        INTROVERTED
    }

    public enum Mood 
    {
        Sad,
        Neutral, 
        Happy    
    }

    [CreateAssetMenu(fileName = "newCharacter", menuName = "Stranded/Character", order = 0)]
    public class Character : ScriptableObject, IEquatable<Character>
    {
        public Sprite portrait;
        
        public string firstName;
        public int age;
        [TextArea(1,10)]
        public string description;
        public Personality personality;

        private Mood _mood;
        public Mood Mood
        { get; set; }

        public int[] attributes;

        public bool[] revealedAttribute;

        public int lastRevealed;

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
