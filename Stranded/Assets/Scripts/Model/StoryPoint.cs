using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model 
{
    [CreateAssetMenu(fileName = "newStoryPoint", menuName = "Stranded/StoryPoint", order = 0)]
    public class StoryPoint : BaseSceneParameter
    {
        public Sprite storyArt;
        [TextArea(1,10)]
        public string[] storyText;
        public float playTime;
        public bool isEndPoint = false;
    }
}
