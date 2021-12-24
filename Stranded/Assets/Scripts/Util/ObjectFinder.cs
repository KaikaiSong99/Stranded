using System;
using System.Collections;
using UnityEngine;

namespace Util
{
  public class ObjectFinder<T> where T : MonoBehaviour
  {
    private T _targetObject;
    private bool _foundTargetObject = false;

    private readonly Action<T> _whenDoneCallback;
    private readonly Func<T> _findFunction;
    
    public ObjectFinder(MonoBehaviour instantiator, Func<T> findFunction, Action<T> whenDoneCallback = null)
    {
      _whenDoneCallback = whenDoneCallback;
      _findFunction = findFunction;

      instantiator.StartCoroutine(LookForObject());
    }
    
    private IEnumerator LookForObject()
    {
      while (!_foundTargetObject)
      {
        var obj = _findFunction();
        if (obj != null)
        {
          _targetObject = obj;
          _foundTargetObject = true;
          _whenDoneCallback?.Invoke(obj);
        }

        yield return null;
      }
    }

    public void Use(Action<T> useCallback, string warningMessage = null)
    {
      if (_foundTargetObject)
      {
        useCallback(_targetObject);
      }
      else if (warningMessage != null)
      {
        Debug.LogWarning(warningMessage);
      }
    }
  }
}