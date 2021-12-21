using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

[CreateAssetMenu(fileName = "BaseSceneParameter", menuName = "Stranded/Base/BaseSceneParameter", order = 0)]
public class BaseSceneParameter : ScriptableObject 
{

    public int round;
    // [HideInInspector]
    public List<Character> characters;
}
