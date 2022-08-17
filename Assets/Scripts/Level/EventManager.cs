using System;

public class EventManager
{
    public static event Action<int> OnCubeMerged;
    public static event Action OnLineCrossed;
    public static event Action OnCubePushed;

    public static void CubeMerged(int value)
    {
        OnCubeMerged?.Invoke(value);
    }

    public static void LineCrossed()
    {
        OnLineCrossed?.Invoke();
    }

    public static void CubePushed()
    {
        OnCubePushed?.Invoke();
    }

}