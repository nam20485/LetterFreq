namespace WordListLib
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Extensions
    {
        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, bool ignoreOrder)
        {
            if (ignoreOrder)
            {
                return Enumerable.SequenceEqual(first.OrderBy(firstElement => firstElement),
                                                second.OrderBy(secondElement => secondElement));
            }
            else
            {
                return first.SequenceEqual(second);
            }
        }

        // http://csharphelper.com/blog/2014/11/convert-an-integer-into-an-ordinal-in-c/
        public static string ToOrdinalSuffix(this int value)
        {
            // Start with the most common extension.
            string extension = "th";

            // Examine the last 2 digits.
            int last_digits = value % 100;

            // If the last digits are 11, 12, or 13, use th. Otherwise:
            if (last_digits < 11 || last_digits > 13)
            {
                // Check the last digit.
                switch (last_digits % 10)
                {
                    case 1:
                        extension = "st";
                        break;
                    case 2:
                        extension = "nd";
                        break;
                    case 3:
                        extension = "rd";
                        break;
                }
            }

            return extension;
        }
    }
}
