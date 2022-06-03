using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CronParser
{
    public static class CronExpressionParser
    {
        // This method checks index(corresponding to minutes,hour,day of month etc), gets description and expression for same index and prints it on console
        public static void Parse(string field,int index) {
            try
            {
                StringBuilder description;
                switch (index)
                {
                    case 0:
                        description = new StringBuilder();
                        description.Append(Helper.GetDescription("minute"));
                        description.Append(GetExpression(field, 0, 59, "minute"));
                        Console.WriteLine(description);
                        break;
                    case 1:
                        description = new StringBuilder();
                        description.Append(Helper.GetDescription("hour"));
                        description.Append(GetExpression(field, 0, 23, "hour"));
                        Console.WriteLine(description);
                        break;
                    case 2:
                        description = new StringBuilder();
                        description.Append(Helper.GetDescription("day of month"));
                        description.Append(GetExpression(field, 1, 31, "days"));
                        Console.WriteLine(description);
                        break;
                    case 3:
                        description = new StringBuilder();
                        description.Append(Helper.GetDescription("month"));
                        description.Append(GetExpression(field, 1, 12, "month"));
                        Console.WriteLine(description);
                        break;
                    case 4:
                        description = new StringBuilder();
                        description.Append(Helper.GetDescription("day of week"));
                        description.Append(GetExpression(field, 1, 7, "week"));
                        Console.WriteLine(description);
                        break;
                    case 5:
                        description = new StringBuilder();
                        description.Append(Helper.GetDescription("command"));
                        description.Append(field);
                        Console.WriteLine(description);
                        break;
                }
            }
            catch (Exception exception) {
                Console.WriteLine("Cron parser failed to parse string. Exception:"+exception);
            }
        }

        //This method takes field of cron string and it min and max value and return its occurences as string
        public static StringBuilder GetExpression(string field , int min, int max,string type) {
            StringBuilder description = new StringBuilder();
            int stepSize = 1;
            if (field == "0")
            {
                return description.Append(0);
            }
            if (field.Contains(Constants.Last))
            {
                return HandleLast(field, description, type);
            }
            if (field.Contains(Constants.Weekday))
            {
                return HandleWeekDay(field, description);
            }
            if (field.Contains(Constants.Any))
            {
                field.Replace(Constants.Any, Constants.IncludeAll);
            }
            if (field.Contains(Constants.FieldSeparator))
            {
                string[] values = field.Split(Constants.FieldSeparator);
                foreach (var value in values)
                {
                    description.Append(GetExpression(value,min,max, type));
                }
                return description;
            }
            if (field.Contains(Constants.StepSeparator))
            {
                string stepValue = "";
                int index = field.IndexOf(Constants.StepSeparator);
                for (int i = index + 1; i < field.Length; i++)
                {
                    stepValue = stepValue + field[i].ToString();
                }
                stepSize = Convert.ToInt32(stepValue);
                string newField = field.Substring(0, index);
                if (newField.Contains(Constants.RangeSeparator))
                {
                    string[] rangeValues = newField.Split(Constants.RangeSeparator);
                    min = Convert.ToInt32(rangeValues[0]);
                    max = Convert.ToInt32(rangeValues[1]);
                }
                else if (newField != Constants.IncludeAll)
                {
                    min = Convert.ToInt32(newField);
                }
                for (int i = min; i <= max; i = i + stepSize)
                {
                    description.Append(i + " ");
                }
                return description;
            }
            if (field.Contains(Constants.RangeSeparator))
            {
                return HandleRangeSeperator(field, description, min, max, stepSize);
            }
            if (field == Constants.IncludeAll)
            {
                return HandleIncludeAll(description,min,max,stepSize);
            }
            return description.Append(field + " "); ;
        }

        //Handles "L"(Last) 
        public static StringBuilder HandleLast(string field,StringBuilder description,string type)
        {
            string dataType = type.Contains("month") ? "month" : "week";
            if (field.Length > 1)
            {
                if (field == "LW")
                {
                    return description.Append("Last Weekday of month");
                }
                if (Helper.DaysOfWeek.Any(item => (item.Value == Convert.ToInt32(field[field.IndexOf(Constants.Last) - 1].ToString()))))
                {
                    string value = Helper.DaysOfWeek.FirstOrDefault(item => item.Value == Convert.ToInt32(field[field.IndexOf(Constants.Last) - 1].ToString())).Key;
                    return description.Append(string.Format("Last {0} of month", value));
                }
            }
            else return description.Append(string.Format("Last day of {0}", dataType));
            return null;
        }

        //Handles weekday
        public static StringBuilder HandleWeekDay(string field, StringBuilder description) {
            string[] weekdays = field.Split(Constants.Weekday);
            return description.Append(string.Format("Nearest weekday to day {0} of the month",weekdays[0]));
        }

        //Handles "*"
        public static StringBuilder HandleIncludeAll(StringBuilder description,int min,int max,int stepSize) {
            for (int i = min; i <= max; i = i + stepSize)
            {
                description.Append(i + " ");
            }
            return description;
        }

        //Handles "-"
        public static StringBuilder HandleRangeSeperator(string field, StringBuilder description, int min, int max, int stepSize) {
            string[] rangeValues = field.Split(Constants.RangeSeparator);
            min = Convert.ToInt32(rangeValues[0]);
            max = Convert.ToInt32(rangeValues[1]);
            for (int i = min; i <= max; i = i + stepSize)
            {
                description.Append(i + " ");
            }
            return description;
        }

    }
}
