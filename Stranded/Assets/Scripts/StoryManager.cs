using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class StoryManager : MonoBehaviour
{
    // When the round is done call onRoundEnd?.Invoke(null)
    public static event Action<Round> onRoundEnd;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onRoundInit += Play;
    }


    // Start this story round (is called from GameManager)
    public void Play(BaseSceneParameter parameters)
    {

    }

    void OnDestroy() {
        GameManager.onRoundInit -= Play;
    }
}
