using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Model;

public class TitleManager : MonoBehaviour
{

    public TitleScriptable TitleText;

    public CanvasGroup LoadingScreen;
    public CanvasGroup PinScreen;

    public static event Action<string> onPinConfirmed;
    public static event Action onGameStart;

    public TextMeshProUGUI ErrorText;
    public TextMeshProUGUI SendPinText;
    public TextMeshProUGUI WaitingText;
    public TextMeshProUGUI CancelText;
    public TextMeshProUGUI PinText;
    public Button CancelButton;
    

    private const string ChannelPrefix = "game."; 
    private bool isGameStart = false;

    void Start()
    {
        SendPinText.text = TitleText.sendPinButtonText;
        WaitingText.text = TitleText.waitingForServerText;
        CancelText.text = TitleText.cancelWaitingText;
        NetworkManager.onStartReceived += OnStartGame;
        PinManager.onPinSent += SendPin;
        isGameStart = false;
    }

    private bool isSanitized(string text) 
    {
        if (text.Length < 4)
        {
            ErrorText.text = TitleText.errorLengthIncorrectText;
            IEnumerator RemoveErrorWithDelay()
            {
                yield return new WaitForSeconds(5f);
                ErrorText.text = "";
            }

            StartCoroutine(RemoveErrorWithDelay());
            return false;
        }

        return true;
    }

    public void OpenPinScreen()
    {
        PinScreen.gameObject.SetActive(true);
    }

    public void SendPin(string pin)
    {
        PinScreen.gameObject.SetActive(false);
        PinText.text = pin;

        if (pin == "0000")
        {
            EnableLoadingScreen();
            CancelButton.interactable = false;
            OnStartGame();
        }
        else if (isSanitized(pin))
        {
            ErrorText.text = "";
            onPinConfirmed?.Invoke(ChannelPrefix + pin);
            EnableLoadingScreen();
        }
    }

    public void EnableLoadingScreen()
    {
        CancelButton.interactable = true;
        LoadingScreen.gameObject.SetActive(true);
    }


    public void DisableLoadingScreen()
    {
        LoadingScreen.gameObject.SetActive(false);
    }


    private void OnStartGame()
    {

        if(!isGameStart && LoadingScreen && LoadingScreen.gameObject.active)
        {
            isGameStart = true;
            StartCoroutine(NotifyGameManagerStart());
        }
    }

    private IEnumerator NotifyGameManagerStart()
    {
        CancelButton.interactable = false;
        Debug.Log("Notify Start");
        WaitingText.text = TitleText.startGameText;
        yield return new WaitForSeconds(5);
        onGameStart?.Invoke();
        yield return null;
    }

    private void OnDestroy() {
        PinManager.onPinSent -= SendPin;
        NetworkManager.onStartReceived -= OnStartGame;
    }


}
