using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FadeController : MonoBehaviour
{

    public Animator fadeAnimator;

    public IEnumerator FadeScreen(bool fadeIn)
    {
        fadeAnimator.SetBool("FadeIn", fadeIn);
        yield return new WaitForSeconds(1);
    }
}
