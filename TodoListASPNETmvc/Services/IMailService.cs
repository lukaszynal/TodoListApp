using System.Threading.Tasks;
using TodoListASPNETmvc.Models;

namespace TodoListASPNETmvc.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
