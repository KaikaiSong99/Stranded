using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Model;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public struct GameScene 
    {
        public string Name;        
        public BaseSceneParameter Parameter;
    }

    public List<Character> characters;
    public List<GameScene> scenes;
    public int roundNumber;
    public static event Action<BaseSceneParameter> onRoundInit;

    private List<Round> progress;
    private bool roundIsFinished;

    void Start()
    {
        RoundManager.onRoundEnd += AdvanceRound;    
        StoryManager.onRoundEnd += AdvanceRound;    
        progress = new List<Round>();
        roundNumber = 1;
        StartCoroutine(Play());

    }

    private IEnumerator Play()
    {
        Debug.Log($"Number of scenes: {scenes.Count}");
        foreach (var scene in scenes)
        {
            SetSceneParameters(scene.Parameter);

            roundIsFinished = false;
            yield return StartCoroutine(LoadScene(scene.Name));

            onRoundInit?.Invoke(scene.Parameter);
            yield return new WaitUntil(() => roundIsFinished);

            yield return StartCoroutine(UnloadScene(scene.Name));
        }

        Debug.Log("End Game");
        yield return null;
    }

    private void SetSceneParameters(BaseSceneParameter parameters)
    {
        parameters.characters = characters;
        parameters.round = roundNumber;
    }

    private IEnumerator LoadScene(string name)
    {
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        var scene = SceneManager.LoadSceneAsync(name, parameters);

        yield return new WaitWhile(() => !scene.isDone);
    }


    private IEnumerator UnloadScene(string name)
    {
        var scene = SceneManager.UnloadSceneAsync(name);

        yield return new WaitWhile(() => !scene.isDone);
    }

    /***
        Advances the round to the next.
        Should be called via an event;
    ***/
    public void AdvanceRound(Round round)
    {
        if (round != null)
        {
            progress.Add(round);
            roundNumber++;           
        }
        roundIsFinished = true;
    }

    void OnDestroy() 
    {
        RoundManager.onRoundEnd -= AdvanceRound;    
        StoryManager.onRoundEnd -= AdvanceRound;    
    }

}
