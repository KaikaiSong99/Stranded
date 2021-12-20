using UnityEngine;


namespace Model 
{
    [CreateAssetMenu(fileName = "newJob", menuName = "Stranded/Job", order = 0)]
    public class Job : ScriptableObject
    {
        public Sprite jobIcon;
        [TextArea(1,10)]
        public string description;

        public Character idealCharacter;

        [TextArea(1,10)]
        public string idealAssignedDialogue;
        [TextArea(1,10)]
        public string idealSuggestDialogue;

        [SerializeField]
        private int _id = -1;
        public int Id
        { get; set; }

      

    }
}
