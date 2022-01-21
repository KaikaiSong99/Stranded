namespace UI
{
  public static class UIElements
  {
    public static void SetInteractable(this IHasCanvasGroup obj, bool enable)
    {
      obj.CanvasGroup.interactable = enable;
      obj.CanvasGroup.blocksRaycasts = enable;
    }
  }
}