using System;

namespace Emobi.Extensions;

public static class StringExtensions
{
    public static T IfNotNullOrEmpty<T>(this string value, Func<string, T> func, T defaultValue = default)
    {
        return string.IsNullOrEmpty(value) ? defaultValue : func(value);
    }
}
