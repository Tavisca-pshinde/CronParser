using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CronParser
{
    //This class will contain all the helper methods for cron parser
    public static class Helper
    {
        public static readonly string[] TokenTypes = new string[] { "*", "-", "?", "/", "L",",", " " };

        public static readonly Dictionary<string, int> DaysOfWeek = new string[]
           {
                "sunday", "monday" ,"tuesday","wednesday" ,"thursday","friday","saturday"
           }.Select((dayOfWeek, index) => new { dayOfWeek, index }).ToDictionary(x => x.dayOfWeek, x => x.index + 1, StringComparer.OrdinalIgnoreCase);

      
        public static StringBuilder GetDescription(string text)
        {
            StringBuilder description = new StringBuilder(text);
            int length = text.Length;
            for (int i = length; i < 14; i++)
            {
                description.Append(" ");
            }
            return description;
        }

    }
}
