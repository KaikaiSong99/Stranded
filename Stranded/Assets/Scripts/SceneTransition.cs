using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{

    public Animator animator;

    public IEnumerator FadeIn()
    {
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeOut()
    {
        animator.SetTrigger("FadOut");
        yield return new WaitForSeconds(1);
    }
}
