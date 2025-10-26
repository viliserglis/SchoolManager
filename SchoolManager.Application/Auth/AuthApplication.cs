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

    public int CreateUser(string username, string password, Roles role)
    {
        
        var existingUser = userRepository.GetByUsername(username);
        if (existingUser != null)
            throw new InvalidOperationException($"Username {username} already exists");

        var user = new User
        {
            Username = username,
            Password = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role
        };

        return userRepository.CreateUser(user);
    }

    public bool SetPassword(int userId, string newPassword)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
        return userRepository.SetPassword(userId, hashedPassword);
    }

    public bool UpdateUserRole(int userId, Roles newRole)
    {
        return userRepository.UpdateUserRole(userId, newRole);
    }
}
