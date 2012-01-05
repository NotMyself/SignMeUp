namespace SignMeUp.Core
{
    public interface IUserRepository
    {
        void Save(User user);
        bool UserExists(User user);
    }
}
