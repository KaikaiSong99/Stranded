using UnityEngine;

namespace Util
{
  public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
  {
    private static T _instance;
    
    public static T Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = (T) FindObjectOfType(typeof(T));

          if (_instance == null)
          {
            Debug.LogError("An instance of " + typeof(T) + 
                           " is needed in the scene, but there is none.");
          }
        }

        return _instance;
      }
    }
  }
}