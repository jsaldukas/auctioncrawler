using System;
using System.Collections.Generic;
using System.Linq;

namespace AuctionCrawler.ConsoleApp.Extensions
{
    static class StringExtensions
    {
        public static string StripNonNumeric(this string s)
        {
            return new string(s.Where(c => Char.IsDigit(c) || c == '.').ToArray());
        }
        
        public static string StripWhitespace(this string s)
        {
            return new string(s.Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }

        public static decimal ToDecimal(this string s, decimal defaultValue = 0)
        {
            decimal d = defaultValue;
            decimal.TryParse(s, out d);
            return d;
        }

        public static int ToInt(this string s, int defaultValue = 0)
        {
            int val = defaultValue;
            int.TryParse(s, out val);
            return val;
        }

        public static TimeSpan? ParseTimeSpanDMS(this string s)
        {
            var parts = s.ToLower().Split(' ').Select(x => x.Trim()).ToList();

            int days = 0;
            int hours = 0;
            int minutes = 0;

            if(parts[0].EndsWith("d"))
            {
                days = int.Parse(parts[0].StripNonNumeric());
                parts.RemoveAt(0);
            }
            
            if (parts[0].EndsWith("h"))
            {
                hours = int.Parse(parts[0].StripNonNumeric());
                parts.RemoveAt(0);
            }
            
            if (parts[0].EndsWith("min"))
            {
                minutes = int.Parse(parts[0].StripNonNumeric());
                parts.RemoveAt(0);
            }

            return new TimeSpan(days, hours, minutes, 0);
        }
        
        public static string ToNewString(this IEnumerable<char> characters)
        {
            return new string(characters.ToArray());
        }
    }
}
