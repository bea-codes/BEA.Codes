using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

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

    public static string RemoveDiacritics(this string text)
    {
        string normalizedString = text.Normalize(NormalizationForm.FormD);

        var stringBuilder = new StringBuilder(normalizedString.Length);

        foreach (char c in normalizedString)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString();
    }

    public static string RemoveSpaces(this string input)
    {
        var brokenInput = input.Split(' ');

        if (brokenInput.Length == 1)
        {
            return brokenInput[0];
        }

        string output = string.Empty;

        foreach (string part in brokenInput)
        {
            output += part.ToUpperFirstLetter();
        }

        return output;
    }

    public static string RemoveSpecialCharacters(this string input, string excepts = "")
    {
        return Regex.Replace(input, $"[^a-zA-Z0-9{excepts}]", "");
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

    public static string ReplaceSpaces(this string input)
    {
        return input.Replace(" ", "_");
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

        return ToUpperFirstLetterEachWord(value, exceptions);
    }

    public static string ToUpperFirstLetter(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return char.ToUpper(value[0]) + value.Substring(1).ToLower();
    }
    //public static string ToUpperFirstLetter(this string input)
    //{
    //    if (string.IsNullOrEmpty(input))
    //    {
    //        return string.Empty;
    //    }

    //    return $"{input[0].ToString().ToUpper()}{input.Substring(1)}";
    //}

    public static string ToUpperFirstLetterEachWord(this string? value, IEnumerable<string> exceptions)
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
    //public static string ToUpperFirstLetterEachWord(this string input, IEnumerable<string> exceptions = null)
    //{
    //    if (string.IsNullOrEmpty(input))
    //        return string.Empty;

    //    if (exceptions is null)
    //        exceptions = Enumerable.Empty<string>();

    //    input = input.ToLower();

    //    var brokenInput = input.Split(' ');

    //    if (brokenInput.Length == 1)
    //    {
    //        return brokenInput[0].ToUpperFirstLetter();
    //    }

    //    string output = string.Empty;

    //    foreach (string part in brokenInput)
    //    {
    //        output += " " + (exceptions.Any(e => e == part) ? part : part.ToUpperFirstLetter());
    //    }

    //    return output;
    //}

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

    public static string TryParseDateTime(this string s, string format)
    {
        if (string.IsNullOrEmpty(s)) return s;
        if (DateTime.TryParse(s, out var dateTimeValue) == false) return s;

        return dateTimeValue.ToString(format);
    }

    public static string TryParseDecimal(this string s, string format)
    {
        if (string.IsNullOrEmpty(s)) return s;
        if (Decimal.TryParse(s, out var decimalValue) == false) return s;

        return decimalValue.ToString(format);
    }
}
