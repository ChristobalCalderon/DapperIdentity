using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string name, string subject, string message, string imageBase64);
        Task SendActivationMail(string email, string name, string token, string html);
        Task SendChangePasswordMail(string email, string name, string token, string html);
    }
}
