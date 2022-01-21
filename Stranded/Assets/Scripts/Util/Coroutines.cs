using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Util
{
  public static class Coroutines
  {
    public static void Delay(this MonoBehaviour owner, float duration, Action action)
    {
      IEnumerator DelayCoroutine()
      {
        yield return new WaitForSeconds(duration);
        action();
      }

      owner.StartCoroutine(DelayCoroutine());
    }
    
    public static void SkipOneFrame(this MonoBehaviour owner, Action action)
    {
      IEnumerator SkipOneFrameCoroutine()
      {
        yield return null;
        action();
      }

      owner.StartCoroutine(SkipOneFrameCoroutine());
    }
    
    public static void InterpolateLinear(this MonoBehaviour owner, float duration, Action<float> update, 
      Action end = null)
    {

      IEnumerator InterpolateLinearCoroutine()
      {
        var t = 0f;

        while (t < duration)
        {
          t += Time.deltaTime;
          update(t / duration);

          yield return null;
        }

        update(1f);
        end?.Invoke();
      }
      
      owner.StartCoroutine(InterpolateLinearCoroutine());
    }

    public static void InterpolateSinus(this MonoBehaviour owner, float duration, Action<float> update, 
      Action end = null)
    {
      owner.InterpolateLinear(duration, t =>
      {
        update((-math.cos(t * math.PI) + 1) / 2);
      }, end);
    }
  }
}