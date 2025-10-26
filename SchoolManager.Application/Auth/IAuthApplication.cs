using SchoolManager.Models.auth;

namespace SchoolManager.Application.Auth;

public interface IAuthApplication
{
    User ValidateUser(string username, string password);

    int CreateUser(string username, string password, Roles role);

    bool SetPassword(int userId, string newPassword);

    bool UpdateUserRole(int userId, Roles newRole);
}
