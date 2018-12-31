using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core;
using WebApp.Core.Models;

namespace WebApp.Infrastructure
{
    public class SmtpMailService : IEmailSender
    {
        private readonly IOptions<EmailSettings> _emailSettings;

        public SmtpMailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(string email, string name, string subject, string message, string imageBase64)
        {
            var client = new SendGridClient(_emailSettings.Value.ApiKey);
            var from = new EmailAddress("christobal_c@hotmail.com", "test");
            var to = new EmailAddress(email, name);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, "");
            msg.AddAttachment(new Attachment()
            {
                Content = imageBase64,
                Type = "image/svg",
                Filename = "signature.svg",
                Disposition = "inline",
                ContentId = "Signature"
            });
            try
            {

                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                Log.Error($"Mail image failed: {ex.Message}");
            }
        }

        public async Task SendActivationMail(string email, string name, string token, string html)
        {
            string subject = "Aktiveringsmail";

            StringBuilder sb = new StringBuilder("Hej,\n\n");
            sb.Append("Du är nästan klar. Konfirmera din e-postadress nedan för att aktivera ditt konto och slutföra registreringen.\n\n");
            string link = $@"https://teamleitesclient.azurewebsites.net/activation?email={email}&code={token}";
            sb.Append(link);

            var client = new SendGridClient(_emailSettings.Value.ApiKey);
            var from = new EmailAddress("no-replay@teamleites.com", "Team Leites");
            var to = new EmailAddress(email, name);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, sb.ToString(), html);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendChangePasswordMail(string email, string name, string token, string html)
        {
            string subject = "Aktiveringsmail";
            string message = "Klicka här för att aktivera ditt konto http://.se/#/changepassword/" + token;

            var client = new SendGridClient(_emailSettings.Value.ApiKey);
            var from = new EmailAddress("no-replay@teamleites.com", "Team Leites");
            var to = new EmailAddress(email, name);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, html);
            var response = await client.SendEmailAsync(msg);

        }
    }
}
