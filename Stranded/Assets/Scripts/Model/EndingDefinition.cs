using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
  [CreateAssetMenu(fileName = "EndingDefinition", menuName = "Stranded/Ending/EndingDefinition", order = 0)]
  public class EndingDefinition : ScriptableObject
  {
    public StoryPoint ending;
    public List<Dilemma> requiredSuccessfulDilemmas;
  }
}