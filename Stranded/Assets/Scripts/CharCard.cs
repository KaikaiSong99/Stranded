using System.Collections;
using System.Collections.Generic;
using Model;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharCard : MonoBehaviour, IPointerDownHandler
{
   
    public Text characterName;
    public Image portrait;
   
    public Button infoButton;
    private Character _character;
    public CanvasGroup infoUI;
    
    
    public Character character
    {
        get { return _character;}
        set { 
            _character = value; characterName.text = value.name;
            portrait.sprite = value.portrait;
            infoButton.GetComponentInChildren<Text>().text = "?";

        }
    }
    void Start () {
        Button btn = infoButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
        
        Debug.Log(infoUI.name);
        infoUI.gameObject.SetActive(true);
    }
  
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("PointerDown");
    }

}
