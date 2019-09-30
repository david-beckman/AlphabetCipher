//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AlphabetCipher
{
    using System;
    using System.Linq;

    /// <summary>General extensions for the <see cref="string" /> class.</summary>
    public static class StringExtensions
    {
        /// <summary>Repeats a string multiple times to fill a resulting string of the passed <paramref name="length" />.</summary>
        /// <param name="value">The string to repeat.</param>
        /// <param name="length">The length of the resulting string.</param>
        /// <returns>A string of length <paramref name="length" /> containing the passed value repeated to fill it.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="value" /> is <value>null</value> or <see cref="string.Empty" />.
        /// </exception>
        public static string RepeatToLength(this string value, int length)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Repeat((int)Math.Ceiling(length * 1.0d / value.Length)).Substring(0, length);
        }

        private static string Repeat(this string value, int times)
        {
            return string.Join(string.Empty, Enumerable.Range(0, times).Select(_ => value));
        }
    }
}
