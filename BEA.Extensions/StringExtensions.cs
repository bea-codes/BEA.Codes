namespace BEA.Extensions;

public static class StringExtensions
{
    public static T IfNotNullOrEmpty<T>(this string? value, Func<string, T> func, T defaultValue = default)
    {
        return string.IsNullOrEmpty(value) ? defaultValue : func(value);
    }

    public static string IfNullOrEmpty(this string? value, string defaultValue = default)
    {
        return string.IsNullOrEmpty(value) ? defaultValue : value;
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

    public static string ToPersonNameCapitalization(this string? value)
    {
        List<string> exceptions = [
            "da",
            "das",
            "de",
            "do",
            "dos",
            "e",
        ];

        return ToUpperFirstCharEachWord(value, exceptions);
    }
    public static string ToUpperFirstChar(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return char.ToUpper(value[0]) + value.Substring(1).ToLower();
    }

    public static string ToUpperFirstCharEachWord(this string? value, List<string> exceptions)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var words = value.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > 0)
            {
                if (exceptions.Contains(words[i].ToLower()))
                    continue;

                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            }
        }
        return string.Join(" ", words);
    }

    public static string TruncateSmart(this string? value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        if (value.Length <= maxLength) return value;

        // Find the last space within the limit
        int lastSpace = value.LastIndexOf(' ', maxLength);
        if (lastSpace > 0) // Found a space, cut there
        {
            return value.Substring(0, lastSpace) + "...";
        }
        else // No space found, cut at maxLength
        {
            return value.Substring(0, maxLength) + "...";
        }
    }
}
