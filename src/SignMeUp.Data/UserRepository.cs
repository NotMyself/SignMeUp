using System.Linq;
using SignMeUp.Core;

namespace SignMeUp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public void Save(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public bool UserExists(User user)
        {
            return context.Users.Any(x => x.Email.Equals(user.Email));
        }
    }
}
