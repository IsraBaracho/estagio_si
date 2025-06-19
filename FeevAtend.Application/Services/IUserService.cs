using System.Threading.Tasks;
using FeevAtend.Domain.Entities;
using FeevAtend.Application.DTOs;

namespace FeevAtend.Application.Services
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        bool VerifyPassword(string password, string passwordHash);
        Task<User> CreateUser(RegisterRequest request);
    }
}
