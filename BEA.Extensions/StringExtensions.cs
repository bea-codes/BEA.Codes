namespace Emobi.Extensions;

public static class StringExtensions
{
    public static T IfNotNullOrEmpty<T>(this string value, Func<string, T> func, T defaultValue = default)
    {
        return string.IsNullOrEmpty(value) ? defaultValue : func(value);
    }

    public static void ReplaceLastOccurrence(this string value, string find, string replace)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(find))
            return;

        int place = value.LastIndexOf(find, StringComparison.Ordinal);

        if (place == -1)
            return;

        value = value.Remove(place, find.Length).Insert(place, replace);
    }
}
