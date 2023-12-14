using Application.UseCases.User;
using Domain.Entities;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUser(int userId);
        Task<int> Register(User user, string password);
        Task<TokenModel> Login(string email, string password);
        //Task<List<UserDto>> SearchUsers(string searchText);
    }
}
