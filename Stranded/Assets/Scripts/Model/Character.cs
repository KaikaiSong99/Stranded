using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{    
    public enum Gender 
    {
        MALE,
        FEMALE
    }

    public enum Race
    {
        WHITE,
        BLACK,
        MIDDLE_EASTERN,
        ASIAN,
        HISPANIC
    }

    public enum Personality
    {
        QUIRKY,
        HASTY,
        EXTROVERT,
        INTROVERT
    }

    public class Character : MonoBehaviour, IEquatable<Character>
    {

        public string m_name;
        public int m_age;
        public string m_description;

        // TODO find better ways to utilize these attributes
        public Gender m_gender;
        public Race m_race;
        public Personality m_personality;

        // Uncomment when merging with Attribute
        // public List<Attributes> m_proficiency;

        private int m_id;

        // Start is called before the first frame update
        void Start()
        {
            // Uncomment when merging with Attribute
            // m_proficiency = new List<Attributes>();
        }

        public void SetId(int id) 
        {
            m_id = id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Character);
        }

        public override int GetHashCode() {
            return m_id;
        }

        public bool Equals(Character other) 
        {
            if (other == null)
                return false;
            return m_id == other.m_id;
        }
        

    }
}
