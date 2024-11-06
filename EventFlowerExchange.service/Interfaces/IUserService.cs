using EventFlowerExchange.services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.Repositories.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(UserDto userDto);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
    }
}
