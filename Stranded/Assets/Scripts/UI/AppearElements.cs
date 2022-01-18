using System;

namespace UI
{
  public static class AppearElements
  {
    public static void LinkTo(this IAppearElement el1, IAppearElement el2, Action finalContinue)
    {
      el1.Continue = el2.Appear;
      el2.Continue = finalContinue;
    }
    
    public static void LinkToImmediate(this IAppearElement el1, IAppearElement el2, Action finalContinue)
    {
      el1.Continue = el2.AppearImmediately;
      el2.Continue = finalContinue;
    }
  }
}