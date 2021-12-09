using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Model;

public class AssignmentManager : MonoBehaviour
{
    public RoundManager roundManager;
    public CanvasGroup assignmentUI;

    private Character _currentCharacter;
    private Job _currentJob;

    public void ShowOverview(Character character)
    {
        _currentCharacter = character;
        assignmentUI.gameObject.SetActive(true);
    }

    public void HideOverview()
    {
        assignmentUI.gameObject.SetActive(false);
    }

    public void MakeAssignment() 
    {
        roundManager.AddAssignment(_currentCharacter, _currentJob);
        HideOverview();
    }
}
