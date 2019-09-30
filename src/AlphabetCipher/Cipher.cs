//-----------------------------------------------------------------------
// <copyright file="Cipher.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AlphabetCipher
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     The Lewis Carol alphabetic cipher as per <see href="https://en.wikipedia.org/wiki/Vigenère_cipher" />. This is a symetric
    ///     poly-alphabetic substitution cipher.
    /// </summary>
    public class Cipher
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>Encodes the passed clear text to cipher text using the passed <paramref name="key" />.</summary>
        /// <param name="key">The symetric key used for encoding or decoding.</param>
        /// <param name="clearText">The text to encode.</param>
        /// <returns>The cipher text.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="key" /> or <paramref name="clearText" /> is <value>null</value> or <see cref="string.Empty" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     One or more characters in either <paramref name="key" /> or <paramref name="clearText" /> are not within the 26 lowercase
        ///     characters.
        /// </exception>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needed for signature.")]
        public string Encode(string key, string clearText)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(clearText))
            {
                throw new ArgumentNullException(nameof(clearText));
            }

            return Transform(key, clearText);
        }

        /// <summary>Decodes the passed cipher text to clear text using the passed <paramref name="key" />.</summary>
        /// <param name="key">The symetric key used for encoding or decoding.</param>
        /// <param name="cipherText">The text to decode.</param>
        /// <returns>The cipher text.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="key" /> or <paramref name="cipherText" /> is <value>null</value> or <see cref="string.Empty" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     One or more characters in either <paramref name="key" /> or <paramref name="cipherText" /> are not within the 26 lowercase
        ///     characters.
        /// </exception>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needed for signature.")]
        public string Decode(string key, string cipherText)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(cipherText))
            {
                throw new ArgumentNullException(nameof(cipherText));
            }

            return Transform(key, cipherText, true);
        }

        /// <summary>Using the clear and cipher text, decipher them to capture the symetric key used.</summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="clearText">The clear text.</param>
        /// <returns>The symetric key used to encode the clear text to the cipher text.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="cipherText" /> or <paramref name="clearText" /> is <value>null</value> or <see cref="string.Empty" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     One or more characters in either <paramref name="cipherText" /> or <paramref name="clearText" /> are not within the 26 lowercase
        ///     characters.
        /// </exception>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needed for signature.")]
        public string Decipher(string cipherText, string clearText)
        {
            /* TODO
             * Reduce the number of allocations if this is in a critical section
             */

            if (string.IsNullOrEmpty(cipherText))
            {
                throw new ArgumentNullException(nameof(cipherText));
            }

            if (string.IsNullOrEmpty(clearText))
            {
                throw new ArgumentNullException(nameof(clearText));
            }

            var repeatedKey = Transform(clearText, cipherText, true);
            var current = repeatedKey.IndexOf(repeatedKey[0], 1);
            int previous;

            while (!repeatedKey.Substring(0, current).RepeatToLength(repeatedKey.Length).Equals(repeatedKey, StringComparison.Ordinal))
            {
                current++;

                do
                {
                    previous = current;
                    current = repeatedKey.IndexOf(repeatedKey.Substring(0, previous), current, StringComparison.Ordinal);
                }
                while (previous != current);
            }

            return repeatedKey.Substring(0, current);
        }

        private static string Transform(string key, string text, bool inverted = false)
        {
            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                var index = Alphabet.IndexOf(text[i]);
                if (index < 0)
                {
                    throw new NotSupportedException("Unsupported Character: " + text[i]);
                }

                var offset = Alphabet.IndexOf(key[i % key.Length]);
                if (offset < 0)
                {
                    throw new NotSupportedException("Unsupported Character: " + key[i % key.Length]);
                }

                if (inverted)
                {
                    offset = Alphabet.Length - offset;
                }

                result[i] = Alphabet[(index + offset) % Alphabet.Length];
            }

            return new string(result);
        }
    }
}
