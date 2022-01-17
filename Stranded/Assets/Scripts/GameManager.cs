using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public GameScene(string name, BaseSceneParameter parameter)
        {
            Name = name;
            Parameter = parameter;
        }
    }

    public List<Character> characters;
    public List<GameScene> scenes;
    public int roundNumber;
    public static event Action<BaseSceneParameter> onRoundInit;
    public static event Action onGameEnd;

    public EndingEvaluationOrder endingEvaluationOrder;
    public bool DebugMode = false;

    private readonly List<Round> progress = new List<Round>();
    private bool roundIsFinished;

    void Start()
    {
        RoundManager.onRoundEnd += AdvanceRound;    
        StoryManager.onRoundEnd += AdvanceRound;
        TitleManager.onGameStart += StartGame;

        if (DebugMode)
        {
            roundNumber = 1;
            StartCoroutine(Play());
        }
        else 
            StartCoroutine(LoadScene("TitleScene"));

    }

    private void StartGame()
    {
        StartCoroutine(UnloadScene("TitleScene"));
        roundNumber = 1;
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        var decidedEnding = false;
        
        Debug.Log($"Number of scenes: {scenes.Count}");
        for (var i = 0; i < scenes.Count; i++)
        {
            var scene = scenes[i];
            SetSceneParameters(scene.Parameter);

            roundIsFinished = false;
            yield return StartCoroutine(LoadScene(scene.Name));

            onRoundInit?.Invoke(scene.Parameter);
            yield return new WaitUntil(() => roundIsFinished);

            yield return StartCoroutine(UnloadScene(scene.Name));

            // after the final "regular scene", decide which ending scene to use and push it to scenes.
            if (i + 1 == scenes.Count && !decidedEnding)
            {
                scenes.Add(DecideEnding());
                decidedEnding = true;
            }
        }

        Debug.Log("End Game");

        scenes.RemoveAt(scenes.Count - 1);
        onGameEnd?.Invoke();

        StartCoroutine(LoadScene("TitleScene"));
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

    private GameScene DecideEnding()
    {
        var storyPoint = endingEvaluationOrder.endings
            .First(endingDefinition => endingDefinition.requiredSuccessfulDilemmas
                .All(dilemma => progress
                    .Exists(round => round.Dilemma.Equals(dilemma) && round.PartiallySucceeded)))
            .ending;
        
        return new GameScene("StoryScene", storyPoint);
    }
    
    /// <summary>
    /// Advances the round to the next.
    /// Should be called via an event;
    /// </summary>
    /// <param name="round"></param>
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
