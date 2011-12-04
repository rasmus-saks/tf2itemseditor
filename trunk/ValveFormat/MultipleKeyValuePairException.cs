using System;

namespace ValveFormat
{
    /// <summary>
    /// Thrown when multiple key-value pairs have been found on a single line
    /// </summary>
    public class MultipleKeyValuePairException : Exception
    {
        public string Line { get; set; }
        public MultipleKeyValuePairException(string line)
        {
            Line = line;
        }
    }
}
