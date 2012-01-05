namespace SignMeUp.Core.Services
{
    public class UserSignUpService
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;

        public UserSignUpService(IUserRepository userRepository, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
        }

        public void SignUp(User user)
        {
            if(!userRepository.UserExists(user))
                userRepository.Save(user);
            emailService.SendActivationEmail(user);
        }
    }
}
