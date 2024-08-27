using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Data;
using MyVotingSystem.Models;


namespace MyVotingSystem.Accessors
{

    public class UserAccessor : IUserAccessor
    {

        private readonly ApplicationDbContext _context;

        public UserAccessor(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public User? SignIn(string email, string password)
        {
            User? u = _context.Users.Where(u => u.Email == email).Where(u => u.Password == password).FirstOrDefault();
            return u;
        }

        public User? GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }
        public User? GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return Save();
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }

}