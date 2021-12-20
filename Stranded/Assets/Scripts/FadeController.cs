using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FadeController : MonoBehaviour
{

    public Animator fadeAnimator;

    public void FadeScreen(bool fadeIn)
    {
        fadeAnimator.SetBool("FadeIn", fadeIn);
    }
}
