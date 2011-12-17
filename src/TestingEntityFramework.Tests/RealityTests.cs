using NUnit.Framework;
using Shouldly;

namespace TestingEntityFramework.Tests
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
