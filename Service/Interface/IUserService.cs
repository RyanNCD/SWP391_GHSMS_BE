using Repository.DTO;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int UserId);
        Task<int> UpdateAsync(User user);
        Task<int> CreateAsync(User user);
        Task<bool> DeleteByIdAsync(int UserId);
        Task<UserProfileDTO?> GetProfileAsync(int userId);
        Task<bool> UpdateProfileAsync(UserProfileDTO dto);
    }
}
