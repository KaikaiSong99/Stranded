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
    

    // TODO change to callable function after instantiation of characters -> Roundmanager
    public void Start()
    {
      var cardWidth = container.rect.width / numberOfColumns;
      var cardHeight = cardWidth / cardAspectRatio;
      var noOfCards = container.childCount;
      var noOfRows = (int) math.ceil((float) noOfCards / numberOfColumns);

      var containerHeightResize = cardHeight * noOfRows - container.rect.height;
      container.sizeDelta = new Vector2(0, containerHeightResize);
      
      gridLayoutGroup.cellSize = new Vector2(cardWidth, cardHeight); 

      container.offsetMin = new Vector2(container.offsetMin.x, container.offsetMin.y - container.offsetMax.y);
      container.offsetMax = new Vector2(container.offsetMax.x, 0);
    }
  }
}

