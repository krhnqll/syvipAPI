namespace DataAccess.Helper.SMSSERVICE
{
    public interface ISmsService
    {
        Task SendSmsAsync(string phoneNumber, string message);
    }
}
