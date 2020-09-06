using System.Net.Mail;
using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.ServiceInterfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string ToAddress, string Subject, string Body, string CcAddress, string BccAddress, Attachment Attachment1, Attachment Attachment2);
        void OnSubscribed(object source, SubscriptionEventArgs args);
    }
}