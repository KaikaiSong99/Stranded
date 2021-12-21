using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingTransitionController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator fadeAnimator;


    public IEnumerator Trigger(string trigger)
    {
        fadeAnimator.SetTrigger(trigger);
        yield return new WaitForSeconds(1);
    }
}
