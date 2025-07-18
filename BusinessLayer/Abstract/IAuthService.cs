using EntityLayer.DTOs.UserDtos;
using EntityLayer.Tables;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        public Task<UserList> Login(string userName, string password);
        public Task<User> Register(User user);
    }
}
