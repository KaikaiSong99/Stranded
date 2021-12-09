using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class GridLayoutGroupAutoScaler : MonoBehaviour
  {
    public GridLayoutGroup gridLayoutGroup;

    public RectTransform container;
    
    public int numberOfColumns;

    [Tooltip("Width divided by height.")]
    public float cardAspectRatio;
    
    private void Start()
    {
      var cardWidth = container.rect.width / numberOfColumns;
      var cardHeight = cardWidth / cardAspectRatio;
      var noOfCards = container.childCount;
      var noOfRows = (int) math.ceil((float) noOfCards / numberOfColumns);

      var containerHeightResize = cardHeight * noOfRows - container.rect.height;
      container.sizeDelta = new Vector2(0, containerHeightResize);
      
      gridLayoutGroup.cellSize = new Vector2(cardWidth, cardHeight); 
    }
  }
}

