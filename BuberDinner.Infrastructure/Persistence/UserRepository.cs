using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly static List<User> _users = new ();
    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email.Equals(email));
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}