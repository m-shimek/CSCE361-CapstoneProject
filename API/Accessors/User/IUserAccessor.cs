using MyVotingSystem.Models;

namespace MyVotingSystem.Accessors
{

    public interface IUserAccessor
    {
        User? SignIn(string email, string password);
        User? GetUserById(int userId);
        User? GetUserByEmail(string email);
        List<User> GetAllUsers();
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        bool Save();
    }

}