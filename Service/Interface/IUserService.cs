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
        Task<int> CreateAsync(UserDTO dto);
        Task<bool> DeleteByIdAsync(int userId);
        Task<List<UserProfileDTO>> GetAllAsync();
        Task<UserProfileDTO?> GetByIdAsync(int userId);
        Task<int> UpdateAsync(UserProfileDTO dto);
        Task<UserProfileDTO?> GetProfileAsync(int userId);
        Task<bool> UpdateProfileAsync(UserProfileDTO dto);
    }

}
