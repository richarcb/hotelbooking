using UserService.Models;

namespace UserService.Data
{
    public interface IUserRepo
    {
        bool SaveChanges();
        User GetUserById(int userId);
        User GetUserByName(string name);
        void CreateUser(User user);
        IEnumerable<User> GetUsers();
    }
}
