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
    public Character CurrentCharacter
    { get; set; }
    private Job _currentJob;
    private Job CurrentJob
    { get; set; }

    // Start is called before the first frame update
    void Start()
    {
         
    }

    public void ShowOverview()
    {
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
