using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Model;
using UI;

public class AssignmentManager : MonoBehaviour
{
    public RoundManager roundManager;
    public DataDisplay dataDisplay;
    public JobScrollerSelector scrollerSelector;
    public CanvasGroup assignmentUI;

    private Character _currentCharacter;
    private Job _currentJob;

    public void Start()
    {
        HideOverview();
    }   

    public void FixedUpdate()
    {
        // if (scrollerSelector.SelectedJob != null) 
        // {
        //     _currentJob = scrollerSelector.SelectedJob;
        //     dataDisplay.SetJobInfo(_currentJob);
        // }
    }

    public void ShowOverview(Character character)
    {

        dataDisplay.SetCharacterInfo(character);
        _currentCharacter = character;
        assignmentUI.gameObject.SetActive(true);
         _currentJob = scrollerSelector.SelectedJob;
        dataDisplay.SetJobInfo(_currentJob);
    }

    public void HideOverview()
    {
        assignmentUI.gameObject.SetActive(false);
    }

    public void MakeAssignment() 
    {
        _currentJob = scrollerSelector.SelectedJob;
        dataDisplay.SetJobInfo(_currentJob);
        roundManager.AddAssignment(_currentCharacter, _currentJob);
        HideOverview();
    }
}
