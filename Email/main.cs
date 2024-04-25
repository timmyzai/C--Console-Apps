using System;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Emails
{
    public class MailGun
    {
        private string apiKey = "35b9dd1a24b11323240098de298c2180-30b58138-7606abbd";
        private string domain = "sandbox08702dd34d88466abbfa07c7ccf8390d.mailgun.org";
        private string ownerEmail = "timmy.chin@attspace.ventures";
        public void SendTestEmail()
        {
            var sender = "Timmy Chin";
            RestClient client = new RestClient(new Uri("https://api.mailgun.net/v3/"));
            client.AddDefaultHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("api:" + apiKey)));
            RestRequest request = new RestRequest();
            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"{sender} <{ownerEmail}>");
            request.AddParameter("to", "info@my-tally.com");
            request.AddParameter("subject", "Test Email");
            request.AddParameter("text", "This is a test email.");
            request.AddFile("attachment", @"Resume/Student Athlete Resume.docx");
            request.AddFile("attachment", @"Resume/Student Athlete Resume.pdf");
            request.Method = Method.Post;
            var result = client.ExecuteAsync(request).Result;
            if (!result.IsSuccessful)
            {
                throw new System.Exception(result.Content);
            }
        }
    }
}