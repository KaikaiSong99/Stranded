using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Model 
{
    [CreateAssetMenu(fileName = "newTitleText", menuName = "Stranded/UI/TitleScreen", order = 0)]
    public class TitleScriptable : ScriptableObject
    {
        public string errorLengthIncorrectText;
        
        public string sendPinButtonText;
        public string waitingForServerText;
        public string cancelWaitingText; 
        public string startGameText;
    }
}
