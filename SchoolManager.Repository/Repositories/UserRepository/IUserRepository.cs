using SchoolManager.Models.auth;

namespace SchoolManager.Repository.Repositories.UserRepository;

public interface IUserRepository
{
    User GetByUsername(string username);
}
