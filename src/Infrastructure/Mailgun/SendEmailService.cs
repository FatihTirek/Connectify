using System.Text.Json;
using Connectify.src.Application.Services;
using RestSharp;
using RestSharp.Authenticators;

namespace Connectify.src.Infrastructure.Mailgun
{
    public class SendEmailService(IConfiguration configuration) : ISendEmailService
    {
        private readonly string BaseUrl = configuration["Mailgun:BaseUrl"]!;
        private readonly string ApiKey = configuration["Mailgun:ApiKey"]!;
        private readonly string Domain = configuration["Mailgun:Domain"]!;
        private readonly string From = "Connectify <no-reply@mail.connectify.com>";
        private readonly string Subject = "is your Connectify code";
        private readonly string Template = "template.untitled.1710511925762";

        public async Task SendConfirmationCode(string to, string code)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(BaseUrl),
                Authenticator = new HttpBasicAuthenticator("api", ApiKey),
            };

            var request = new RestRequest
            {
                Method = Method.POST,
                Resource = $"{Domain}/messages",
            };

            request.AddParameter("domain", Domain, ParameterType.UrlSegment);
            request.AddParameter("from", From);
            request.AddParameter("to", to);
            request.AddParameter("subject", $"{code} {Subject}");
            request.AddParameter("template", Template);
            request.AddParameter("h:X-Mailgun-Variables", JsonSerializer.Serialize(new { to, code }));

            await client.ExecuteAsync(request);
        }
    }
}