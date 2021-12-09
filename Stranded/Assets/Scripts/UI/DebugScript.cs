using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class DebugScript : MonoBehaviour
  {
    public JobScrollerSelector selector;

    public string jobName;

    private void Update()
    {
      jobName = selector.SelectedJob.name;
    }
  }
}