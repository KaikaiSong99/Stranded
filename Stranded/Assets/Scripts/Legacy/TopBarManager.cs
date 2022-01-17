using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Legacy
{
    public class TopBarManager : MonoBehaviour
    {
        private ObjectFinder<RoundManager> _roundManager;

        private const string RoundPrefix = "Round: ";

        public Text roundText;
        public Text timeText;

        private void Start()
        {
            _roundManager = new ObjectFinder<RoundManager>(this, FindObjectOfType<RoundManager>);
        }

        private void Update()
        {
            _roundManager.Use(roundManager =>
            {
            
                roundText.text = RoundPrefix + roundManager.dilemma?.round;
                timeText.text = DisplayTime(roundManager.timeLeft);
            }, "Top bar couldn't find a round manager in the scene.");
        }

        private static string DisplayTime(float timeToDisplay)
        {
            if (timeToDisplay < 0)
            {
                timeToDisplay = 0;
            }

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            return $"{minutes:00}:{seconds:00}";
        }
    }
}
