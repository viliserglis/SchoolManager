using SchoolManager.Models.auth;

namespace SchoolManager.Application.Auth;

public interface IAuthApplication
{
    User ValidateUser(string username, string password);
}
