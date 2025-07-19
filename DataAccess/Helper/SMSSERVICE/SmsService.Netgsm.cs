//using DataAccess.Helper.SMSSERVICE;
//using Microsoft.Extensions.Configuration;
//using RestSharp;

//public class NetgsmSmsService : ISmsService
//{
//    private readonly IConfiguration _config;

//    public NetgsmSmsService(IConfiguration config)
//    {
//        _config = config;
//    }

//    public void SendSms(string phoneNumber, string message)
//    {
//        var username = _config["NetGsm:Username"];
//        var password = _config["NetGsm:Password"];
//        var header = _config["NetGsm:Header"];

//        var client = new RestClient("https://api.netgsm.com.tr/sms/send/get/");
//        var request = new RestRequest();
//        request.AddParameter("usercode", username);
//        request.AddParameter("password", password);
//        request.AddParameter("gsmno", phoneNumber);
//        request.AddParameter("message", message);
//        request.AddParameter("msgheader", header);

//        client.Execute(request);
//    }
//}
