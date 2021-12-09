using System;
using UnityEngine;

namespace UI
{
  public class DebugScript2 : MonoBehaviour
  {
    private void Start()
    {
      Debug.Log($"Debug: {GetComponent<RectTransform>().rect}");
    }
  }
}