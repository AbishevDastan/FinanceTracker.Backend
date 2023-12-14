using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int userId);
        Task<int> Register(User user, string password);
        Task<User> GetUserByEmail(string email);
        //Task<List<User>> SearchUsers(string searchText);
    }
}
