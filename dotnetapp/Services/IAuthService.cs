using dotnetapp.Models;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(User model, string role);
        Task<(int, string)> Login(LoginModel model);
        Task<List<User>> GetAllUsersAsync();
    }
}
