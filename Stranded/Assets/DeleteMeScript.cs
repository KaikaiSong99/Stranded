using UnityEngine;
using UnityEngine.UI;

public class DeleteMeScript : MonoBehaviour
{
    public ScrollRect scrollRect;
    
    public void OnScroll()
    {
        Debug.Log(scrollRect.verticalNormalizedPosition);
    }
}
