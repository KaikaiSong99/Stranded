using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
  private ScrollRect _scrollRect;
  
  // debug variable
  public float verticalNormalizedPosition;
  

  private void Start()
  {
    _scrollRect = GetComponent<ScrollRect>();
  }

  private void Update()
  {
    verticalNormalizedPosition = _scrollRect.verticalNormalizedPosition;

    if (Input.GetKeyDown(KeyCode.Q))
    {
      _scrollRect.verticalNormalizedPosition = 0.5f;
    }
  }
}