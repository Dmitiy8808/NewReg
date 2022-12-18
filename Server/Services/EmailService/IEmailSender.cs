using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.EmailService
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessage message);
        Task SendEmailAsync(EmailMessage message);
    }
}