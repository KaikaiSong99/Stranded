using UnityEngine;
using UnityEngine.UI;

public class TopBarManager : MonoBehaviour
{
    private RoundManager _roundManager;

    private const string RoundPrefix = "Round: ";

    public Text roundText;
    public Text timeText;

    private void Update()
    {
        if (FindRoundManager())
        {
            if (_roundManager.dilemma != null) 
            {
                roundText.text = RoundPrefix + _roundManager.dilemma.round;
                timeText.text = DisplayTime(_roundManager.timeLeft);
            }
        }
        else
        {
            Debug.LogWarning("Top bar couldn't find a round manager in the scene.");
        }
    }

    private bool FindRoundManager()
    {
        if (_roundManager != null)
            return true;

        _roundManager = FindObjectOfType<RoundManager>();

        return _roundManager != null;
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
