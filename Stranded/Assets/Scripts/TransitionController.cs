using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TransitionController : MonoBehaviour
{

    public Animator fadeAnimator;

    public IEnumerator Trigger(string trigger)
    {
        fadeAnimator.SetTrigger(trigger);
        yield return new WaitForSeconds(1);
    }
}
