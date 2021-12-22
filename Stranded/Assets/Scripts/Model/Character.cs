using System;
using UnityEngine;
using Util;

namespace Model
{    
        // TODO other personality types when useful.

    [CreateAssetMenu(fileName = "newCharacter", menuName = "Stranded/Character", order = 0)]
    public class Character : ScriptableObject, IEquatable<Character>
    {
        public Sprite portrait;
        
        public string firstName;
        public int age;

        [TextArea(1,10)]
        public string description;

        [TextArea(1,10)]
        public string incorrectlyAssignedDialogue;

        private readonly int _id = SimpleIdGenerator.NextId;

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
