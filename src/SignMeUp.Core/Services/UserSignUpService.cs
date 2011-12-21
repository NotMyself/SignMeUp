namespace SignMeUp.Core.Services
{
    public class UserSignUpService
    {
        private readonly IUserRepository userRepository;

        public UserSignUpService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void SignUp(User user)
        {
            userRepository.Save(user);
        }
    }
}
