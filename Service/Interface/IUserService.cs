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
        Task<User> GetByIdAsync(Guid UserId);
        Task<int> UpdateAsync(User user);
        Task<int> CreateAsync(User user);
        Task<bool> DeleteByIdAsync(Guid UserId);
        Task<UserProfileDTO?> GetProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(UserProfileDTO dto);
    }

}
