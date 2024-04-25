using System.Text;
using RestSharp;
using Infobip.Api.Client;
using Infobip.Api.Client.Api;
using Infobip.Api.Client.Model;

namespace SMS
{
    public class TestInfobip
    {
        public void SendTestSMS()
        {
            var sendSmsApi = new SendSmsApi(new Configuration()
            {
                BasePath = "https://xl2rpq.api.infobip.com",
                ApiKeyPrefix = "App",
                ApiKey = "793f1ef22b5bf3a64cc65b97c1118401-89100c32-6075-46e3-8c90-296329f43d50"
            });
            var smsRequest = new SmsAdvancedTextualRequest()
            {
                Messages = new List<SmsTextualMessage>()
                {
                    new SmsTextualMessage()
                    {
                        From = "ByteBank",
                        Destinations = new List<SmsDestination>() { new SmsDestination(to: "60107601629") },
                        Text = "This is a test message"
                    }
                }
            };
            try
            {
                var smsResponse = sendSmsApi.SendSmsMessage(smsRequest);
                Console.WriteLine("Response: " + smsResponse.Messages.FirstOrDefault());
            }
            catch (ApiException apiException)
            {
                Console.WriteLine($"Error occurred! \n\tMessage: {apiException.Message}\n\tError content: {apiException.ErrorContent}");
            }
        }
    }
}