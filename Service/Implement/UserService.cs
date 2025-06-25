using Repository.DTO;
using Repository.Models;
using Repository.Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> CreateAsync(UserDTO dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Gender = dto.Gender,
                PasswordHash = dto.Password,
                RoleId = dto.RoleId,
                PhoneNumber = dto.phoneNumber,
                Address = dto.Address,
                Email = dto.Email,
                CreateAt = DateTime.Now
            };

            return await _repository.CreateAsync(user);
        }

        public async Task<bool> DeleteByIdAsync(int userId)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null) return false;

            await _repository.RemoveAsync(user);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<List<UserProfileDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(user => new UserProfileDTO
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Gender = user.Gender,
                phoneNumber = user.PhoneNumber,
                Address = user.Address,
                Email = user.Email,
                createdAt = user.CreateAt
            }).ToList();
        }

        public async Task<UserProfileDTO?> GetByIdAsync(int userId)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null) return null;

            return new UserProfileDTO
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Gender = user.Gender,
                phoneNumber = user.PhoneNumber,
                Address = user.Address,
                Email = user.Email,
                createdAt = user.CreateAt
            };
        }

        public async Task<int> UpdateAsync(UserProfileDTO dto)
        {
            var user = await _repository.GetByIdAsync(dto.UserId);
            if (user == null) return 0;

            user.FullName = dto.FullName;
            user.Gender = dto.Gender;
            user.PhoneNumber = dto.phoneNumber;
            user.Address = dto.Address;
            user.Email = dto.Email;

            await _repository.UpdateAsync(user);
            return await _repository.SaveAsync();
        }

        public async Task<UserProfileDTO?> GetProfileAsync(int userId)
        {
            return await GetByIdAsync(userId);
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
            await _repository.SaveAsync();
            return true;
        }
    }
}
