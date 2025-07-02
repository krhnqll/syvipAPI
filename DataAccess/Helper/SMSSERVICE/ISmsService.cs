namespace DataAccess.Helper.SMSSERVICE
{
    public interface ISmsService
    {
        void SendSms(string phoneNumber, string message);
    }
}
