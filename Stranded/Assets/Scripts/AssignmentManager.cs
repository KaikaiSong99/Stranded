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
    
    private CharacterCard _currentCharacterCard;
    private Character _currentCharacter;

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
        
        var characterJob = roundManager.GetJobAssignment(_currentCharacter);
        dataDisplay.SetJobInfo(characterJob);
        scrollerSelector.OnDisplay(characterJob);
    }

    public void SetCharacterCard(CharacterCard current)
    {
        _currentCharacterCard = current;
    }

    public void HideOverview()
    {
        assignmentUI.gameObject.SetActive(false);
    }

    public void MakeAssignment()
    {
        var selectedJob = scrollerSelector.SelectedJob;
        dataDisplay.SetJobInfo(selectedJob);
        roundManager.AddAssignment(_currentCharacter, selectedJob);
        _currentCharacterCard.setJob(selectedJob);
        HideOverview();
    }
}
