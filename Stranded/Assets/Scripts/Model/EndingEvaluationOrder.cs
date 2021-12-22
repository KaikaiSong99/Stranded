using System.Collections.Generic;
using UnityEngine;

namespace Model
{
  [CreateAssetMenu(fileName = "EndingEvaluationOrder", menuName = "Stranded/Ending/EndingEvaluationOrder", order = 0)]
  public class EndingEvaluationOrder : ScriptableObject
  {
    public List<EndingDefinition> endings;
  }
}