using Repository.DTO;
using Repository.Models;
using Repository.Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        public UserService(UserRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> CreateAsync(User user)
        {
            return await _repository.CreateAsync(user);
        }

        public async Task<bool> DeleteByIdAsync(int UserId)
        {
            var user = _repository.GetById(UserId);
            if (user == null)
            {
                return false;
            }
            await _repository.RemoveAsync(user);
             await _repository.SaveAsync();
            return true;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int UserId)
        {
            return await _repository.GetByIdAsync(UserId);
        }

        public async Task<int> UpdateAsync(User user)
        {
            await _repository.UpdateAsync(user);
            return await _repository.SaveAsync(); 
        }
        public async Task<UserProfileDTO?> GetProfileAsync(int userId)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null) return null;

            return new UserProfileDTO
            {
                Address = user.Address,
                createdAt = user.CreateAt,
                Email = user.Email,
                FullName = user.FullName,
                Gender = user.Gender,
                phoneNumber = user.PhoneNumber

            };
        }

        public async Task<bool> UpdateProfileAsync(UserProfileDTO dto)
        {
            var user = await _repository.GetByIdAsync(dto.UserId);
            if (user == null) return false;

            user.FullName = dto.FullName;
            user.Gender = dto.Gender;
            
            user.PhoneNumber = dto.phoneNumber;
            user.Address = dto.Address;
           

            await _repository.UpdateAsync(user);
            return true;
        }
    }
}
