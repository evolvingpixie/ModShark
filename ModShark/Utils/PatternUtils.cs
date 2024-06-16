﻿using System.Text.RegularExpressions;

namespace ModShark.Utils;

public static class PatternUtils
{
    /// <summary>
    /// Creates a pattern that matches any one of a collection of patterns.
    /// If only one pattern is provided, then it is returned as-is.
    /// If no patterns are provided, then returns and empty string.
    /// </summary>
    public static string AnyOf(ICollection<string> patterns)
        => patterns.Count switch
        {
            0 => "",
            1 => patterns.Single(),
            _ => string.Join("|", patterns.Select(p => $"({p})"))
        };

    /// <summary>
    /// Creates a regular expression that matches any of the provided patterns.
    /// Anonymous capturing groups will be disabled.
    /// </summary>
    public static Regex CreateMatcher(ICollection<string> patterns, bool ignoreCase = false)
    {
        var pattern = AnyOf(patterns);
        var options = RegexOptions.Compiled | RegexOptions.ExplicitCapture;
        
        if (ignoreCase)
        {
            options |= RegexOptions.IgnoreCase;
        }

        return new Regex(pattern, options);
    }
}