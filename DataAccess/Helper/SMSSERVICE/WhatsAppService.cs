using DataAccess.Helper.SMSSERVICE;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

public class WhatsAppService : ISmsService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public WhatsAppService(IConfiguration config)
    {
        _config = config;
        _httpClient = new HttpClient();
    }

    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        var token = _config["WhatsApp:AccessToken"];
        var phoneNumberId = _config["WhatsApp:PhoneNumberId"];

        var requestUrl = $"https://graph.facebook.com/v18.0/{phoneNumberId}/messages";

        var requestBody = new
        {
            messaging_product = "whatsapp",
            to = phoneNumber,
            type = "text",
            text = new { body = message }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var response = await _httpClient.PostAsync(requestUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"WhatsApp API Error: {error}");
        }
    }
}
