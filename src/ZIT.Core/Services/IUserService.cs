using ZIT.Core.DTOs;
using ZIT.Core.Entities;

namespace ZIT.Core.Services;

public interface IUserService
{
    Task AddAsync(AddUserDto addUserDto);
    Task<UserDto?> GetAsync(Guid id);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task UpdateAsync(UpdateUserDto updateUserDto);
    Task DeleteAsync(Guid id);
}