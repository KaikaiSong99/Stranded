using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Model;

public class AssignmentManager : MonoBehaviour
{
    public RoundManager roundManager;
    public Character currentCharacter;
    public Job currentJob;
    public CanvasGroup assignmentUI;

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
        roundManager.AddAssignment(currentCharacter, currentJob);
        HideOverview();
    }
}
