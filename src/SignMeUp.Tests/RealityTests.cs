using NUnit.Framework;
using Shouldly;

namespace SignMeUp.Tests
{
    [TestFixture]
    public class RealityTests
    {
        [Test]
        public void true_should_be_true()
        {
            true.ShouldBe(true);
        }
    }
}
