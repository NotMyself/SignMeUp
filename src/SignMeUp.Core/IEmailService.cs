namespace SignMeUp.Core
{
    public interface IEmailService
    {
        void SendActivationEmail(User user);
    }
}
