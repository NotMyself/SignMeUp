using NUnit.Framework;
using TestingEntityFramework.Core;
using TestingEntityFramework.Data;
using TestingEntityFramework.Tests.Helpers;

namespace TestingEntityFramework.Tests
{
    [TestFixture]
    public class UserPersistenceTests
    {
        
        private DataContext context;

        [SetUp]
        public void SetUp()
        {
            context = TestDataContextFactory.Build();
        }

        [Test]
        public void can_persist_user()
        {
            var user = new User { Id = -1, FirstName = "Dirk", LastName = "Diggler", Email = "dirk@diggler.com"};
            
            context.Users.Add(user);
            context.SaveChanges();

            Assert.That(user.Id, Is.EqualTo(1));
        }
    }
}
