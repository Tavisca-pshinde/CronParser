using Xunit;

namespace CronParser.Test
{
    //Added test cases to test program class
    public class ProgramTest
    {
        [Fact]
        public void IsValidInput_Should_Return_True_On_Valid_Input()
        {
            string cronString = "*/15 0 1,15 * 1-5 /usr/bin/find";
            bool isValidInput = Program.IsInputValid(cronString.Split(" "));
            Assert.True(isValidInput);
        }

        [Fact]
        public void IsValidInput_Should_Return_False_On_Invalid_Input()
        {
            string cronString = "*/15 0 1,15 * 1-5 2022 /usr/bin/find";
            bool isValidInput = Program.IsInputValid(cronString.Split(" "));
            Assert.False(isValidInput);
        }
    }
}
