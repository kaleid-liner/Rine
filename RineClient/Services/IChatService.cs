using System.Threading.Tasks;
using RineClient.Models;

namespace RineClient.Services
{
    public interface IChatService
    {
        Task<LoginResult> LoginAsync(string userName, string password);
    }
}
