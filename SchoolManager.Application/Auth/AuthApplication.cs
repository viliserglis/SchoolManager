using SchoolManager.Models.auth;
using SchoolManager.Repository.Repositories.UserRepository;

namespace SchoolManager.Application.Auth;

public class AuthApplication(IUserRepository userRepository) : IAuthApplication
{
    public User ValidateUser(string username, string password)
    {
        var user = userRepository.GetByUsername(username);

        if (user == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            return null;

        return user;
    }
}
