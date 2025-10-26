using SchoolManager.Models.auth;

namespace SchoolManager.Repository.Repositories.UserRepository;

public interface IUserRepository
{
    User GetByUsername(string username);

    IList<Permissions> GetRolePermision(Roles role);

    int CreateUser(User user);

    bool SetPassword(int userId, string newPasswordHash);

    bool UpdateUserRole(int userId, Roles newRole);
}

