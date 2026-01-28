namespace BEA.Extensions;

public static class ConditionExtensions
{
    public static T Then<T>(this bool value, T result)
    {
        return value ? result : default(T);
    }

    public static T Then<T>(this bool value, Func<T> result)
    {
        return value ? result.Invoke() : default(T);
    }
}
