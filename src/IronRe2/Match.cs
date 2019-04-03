using System.Text;

namespace IronRe2
{
    /// <summary>
    /// Match Object
    /// <para>
    ///  Represents the extent of a pattern's match in a given search string.
    /// </para>
    /// </summary>
    public class Match
    {
        protected readonly byte[] _haystack;

        internal Match()
        {
            Matched = false;
        }

        internal Match(byte[] haystack, Re2Ffi.cre2_range_t range)
        {
            _haystack = haystack;
            // If the indexes on the range are invalid then we didn't match
            if (range.start < 0 || range.past < 0)
            {
                Matched = false;
                Start = -1;
                End = -1;
            }
            else
            {
                Matched = true;
                Start = range.start;
                End = range.past;
            }
        }

        /// <summary>
        /// True if the pattern matched the string.
        /// </summary>
        public bool Matched { get; }

        /// <summary>
        ///  If the pattern matched the start index of the match, in bytes.
        /// </summary>
        public long Start { get; }

        /// <summary>
        ///  If the pattern matched the end index of the match, in bytes.
        /// </summary>
        public long End { get; }

        /// <summary>
        /// Get the text for this match
        /// </summary>
        public string ExtractedText => Matched ?
            Encoding.UTF8.GetString(_haystack, (int)Start, (int)(End - Start)):
            string.Empty;

        /// <summary>
        ///  A singleton match which represents all empty matches
        /// </summary>
        public static readonly Match Empty = new Match();
    }
}
