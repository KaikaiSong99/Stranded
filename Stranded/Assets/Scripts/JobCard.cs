using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using Model;

public class JobCard : MonoBehaviour, IPointerDownHandler
{
    
    public Text jobName;
    public Text CharacterName;
    public Image Portrait;
    public Image JobIndicator;
    public Button InfoButton;
    private Job _job;
    public Job job
    {
        get { return _job;}
        set { _job = value; jobName.text = value.name;}
    }
    
    // TODO: show character overview
    public void OnPointerDown(PointerEventData data)
    {
    }
    
}