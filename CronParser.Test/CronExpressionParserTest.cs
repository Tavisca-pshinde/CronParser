using System.Text;
using Xunit;

namespace CronParser.Test
{
    //Added test cases to test CronExpression class
    public class CronExpressionParserTest
    {
        //This method tests for multiple scenarios
        //Parameters : input,output,min,max,type 
        [Theory]
        [InlineData("*/15", "0 15 30 45 ", 0, 59, "minute")]
        [InlineData("0", "0", 0, 23, "hour")]
        [InlineData("1,15", "1 15 ", 1, 31, "days")]
        [InlineData("*", "1 2 3 4 5 6 7 8 9 10 11 12 ", 1, 12, "month")]
        [InlineData("1-5", "1 2 3 4 5 ", 1, 7, "week")]
        [InlineData("14,18,3-39/3,52", "14 18 3 6 9 12 15 18 21 24 27 30 33 36 39 52 ", 0,59, "minute")]
        [InlineData("0/5", "0 5 10 15 20 ", 0, 23, "hour")]
        [InlineData("1,2,3-20", "1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 ", 1, 31, "days")]
        [InlineData("15W", "Nearest weekday to day 15 of the month", 1, 12, "month")]
        [InlineData("3L", "Last TUE of month", 1, 7, "week")]
        public void GetExpression_Should_Return_Description_As_Per_Input(string inputField, string output, int min,int max, string type)
        {
            var outputDescription = CronExpressionParser.GetExpression(inputField, min,max,type);
            Assert.Equal(output, outputDescription.ToString());
        }

        [Fact]
        public void HandleRangeSeperator_Should_Return_Values_With_Given_Range()
        {
            var outputDescription = CronExpressionParser.HandleRangeSeperator("5-10",new StringBuilder(),1,20,1);
            Assert.Equal("5 6 7 8 9 10 ",outputDescription.ToString());
        }

        [Fact]
        public void HandleIncludeAll_Should_Return_All_Values_From_Min_To_Max ()
        {
            var outputDescription = CronExpressionParser.HandleIncludeAll(new StringBuilder(), 1, 5, 1);
            Assert.Equal("1 2 3 4 5 ", outputDescription.ToString());
        }


        [Fact]
        public void HandleWeekDay_Should_Return_Description_As_Per_Input()
        {
            var outputDescription = CronExpressionParser.HandleWeekDay("15W",new StringBuilder());
            Assert.Equal("Nearest weekday to day 15 of the month", outputDescription.ToString());
        }

        //This method tests for multiple scenarios
        //Parameters : input,output,type
        [Theory]
        [InlineData("LW", "Last Weekday of month","month")]
        [InlineData("L", "Last day of week", "week")]
        [InlineData("L", "Last day of month", "month")]
        [InlineData("2L", "Last MON of month", "week")]
        [InlineData("13W", "Last MON of month", "week")]
        public void HandleLast_Should_Return_Description_As_Per_Input(string inputField,string output,string type)
        {
            var outputDescription = CronExpressionParser.HandleLast(inputField, new StringBuilder(),type);
            Assert.Equal(output, outputDescription.ToString());
        }

    }
}
