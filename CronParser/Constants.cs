using System;
using System.Collections.Generic;
using System.Text;

namespace CronParser
{
    //Listed all constants here
    static class Constants
    {
        public static readonly string FieldSeparator = ",";
        public static readonly string RangeSeparator = "-";
        public static readonly string StepSeparator = "/";
        public static readonly string IncludeAll = "*";
        public static readonly string Any = "?";
        public static readonly string Space = " ";
        public static readonly string Last = "L";
        public static readonly string Weekday = "W";
        public static readonly string Hash = "#";
    }
}
