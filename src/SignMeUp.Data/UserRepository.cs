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
    }
}
