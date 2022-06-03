using System.Text;
using Xunit;

namespace CronParser.Test
{
    //Added test cases to test helper class
    public class HelperTest
    {
        [Fact]
        public void GetDescription_Should_Return_Description()
        {
            string inputDescription = "day of week";
            var outputDescription = Helper.GetDescription(inputDescription);
            Assert.Equal("day of week   ",outputDescription.ToString());
        }
    }
}
