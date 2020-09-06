using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Application.ServiceInterfaces;
using Application.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public Task SendEmailAsync(string ToAddress, string Subject, string Body, string CcAddress, string BccAddress, Attachment Attachment1, Attachment Attachment2)
        {
            SendEmail(ToAddress, Subject, Body, CcAddress, BccAddress, Attachment1, Attachment2);
            return Task.CompletedTask;
        }

        public void OnSubscribed(object source, SubscriptionEventArgs args)
        {
            SendEmail(args.ToAddress, args.Subject, args.Body, args.CcAddress, null, null, null);
        }

        private void SendEmail(string ToAddress, string Subject, string Body, string CcAddress, string BccAddress, Attachment Attachment1, Attachment Attachment2)
        {
            var FromAddress = _config["EmailFrom"];
            var Password = _config["EmailPass"];
            var message = new MailMessage
            {
                From = new MailAddress(FromAddress),
                Subject = Subject,
                Body = Body
            };
            message.To.Add(new MailAddress(ToAddress));
            if (!string.IsNullOrEmpty(CcAddress))
                message.CC.Add(new MailAddress(CcAddress));
            if (!string.IsNullOrEmpty(BccAddress))
                message.Bcc.Add(new MailAddress(BccAddress));
            message.IsBodyHtml = true;
            if (Attachment1 != null)
                message.Attachments.Add(Attachment1);
            if (Attachment2 != null)
                message.Attachments.Add(Attachment2);
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromAddress, Password)
            };
            smtpClient.Send(message);
        }
    }
}
