using System.Threading.Tasks;

namespace RestApiDemo.Kernel.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string from, string subject, string body);
    }
}
