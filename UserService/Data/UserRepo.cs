using UserService.Models;

namespace UserService.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateUser(User user)
        {
            if(user == null)
                throw new ArgumentNullException("user");
            _context.Users.Add(user);

        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public User GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Username == name);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
