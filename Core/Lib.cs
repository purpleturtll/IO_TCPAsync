using System;

namespace App
{
    /// <summary>
    /// Class for handling server functionality.
    /// </summary>
    public class Lib
    {
        /// <summary>
        /// Prepare string for sending to client.
        /// </summary>
        /// <param name="s">String received from the client.</param>
        /// <returns>Reversed and capitalized string.</returns>
        public static string Prepare(string s)
        {
            return Reverser.Reverse(Capitlaizer.Capitalize(s));
        }
    }

    /// <summary>
    /// String reverser.
    /// </summary>
    class Reverser 
    {
        /// <summary>
        /// Reverses string.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <returns>Reversed string.</returns>
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    /// <summary>
    /// String capitalizer.
    /// </summary>
    class Capitlaizer
    {
        /// <summary>
        /// Capitalizes string.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <returns>Capitalized string.</returns>
        public static string Capitalize(string s)
        {
            return s.ToUpper();
        }
    }
}
