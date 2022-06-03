using System;

namespace CronParser
{
    
    public static class Program
    {
        //This method is entry point of this app
        //This method takes input, validates it and sends valid string to parser
        static void Main()
        {
            Console.WriteLine("Enter cron string:");
            string cronString = Console.ReadLine();
            string[] listOfFields = cronString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if (IsInputValid(listOfFields))
            {
                for (int index = 0; index < listOfFields.Length; index++)
                {
                    CronExpressionParser.Parse(listOfFields[index], index);
                }
            }
        }

        //Validates input
        public static bool IsInputValid(string[] listOfFields) {
            if (listOfFields.Length < 6 || listOfFields.Length > 6)
            {
                Console.WriteLine("Enter valid cron job with 6 space seperated fields");
                return false;
            }
            return true;
        }
    }
}
