namespace Util
{
  public static class SimpleIdGenerator
  {
    private static int _nextId = 0;

    public static int NextId => _nextId++;
  }
}