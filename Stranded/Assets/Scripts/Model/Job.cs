using UnityEngine;
using Util;


namespace Model 
{
    [CreateAssetMenu(fileName = "newJob", menuName = "Stranded/Job", order = 0)]
    public class Job : ScriptableObject
    {
        public Sprite jobIcon;

        public string jobTitle;

            [TextArea(1,10)]
        public string description;

        public Character idealCharacter;

        [TextArea(1,10)]
        public string idealAssignedDialogue;
        [TextArea(1,10)]
        public string idealSuggestDialogue;

        private readonly int _id = SimpleIdGenerator.NextId;
    }
}
