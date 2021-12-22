using UnityEngine;
using UnityEngine.UI;

public class TopBarManager : MonoBehaviour
{
    private GameManager _gameManager;
    private RoundManager _roundManager;

    private const string RoundPrefix = "Round: ";

    public Text roundText;
    public Text timeText;

    private void Update()
    {
        FindRoundManagerAndGameManager();
        
        roundText.text = RoundPrefix + _gameManager.roundNumber;
        timeText.text = DisplayTime(_roundManager.timeLeft);
    }

    private void FindRoundManagerAndGameManager()
    {
        if (_roundManager == null) _roundManager = 
            _roundManager = FindObjectOfType<RoundManager>();
        
        if (_gameManager == null) _gameManager = 
            _gameManager = FindObjectOfType<GameManager>();
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
