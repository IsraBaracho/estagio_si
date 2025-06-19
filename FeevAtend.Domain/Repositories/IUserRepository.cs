using System;
using System.Threading.Tasks;
using FeevAtend.Domain.Entities;

namespace FeevAtend.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task<User> GetById(Guid id);
Task<User> GetUserById(Guid id);
        Task Create(User user);
        Task Update(User user);
    }
}
