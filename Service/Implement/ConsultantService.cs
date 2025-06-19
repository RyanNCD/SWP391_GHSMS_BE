using Repository.Base;
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
    public class ConsultantService : IConsultantService
    {
        private readonly ConsultantRepository _repository;

        public ConsultantService(ConsultantRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ConsultantDTO>> GetAllAsync()
        {
            var consultants = await _repository.GetAllAsync();
            return consultants.Select(c => new ConsultantDTO
            {
                Id = c.ConsultantId,
                degree = c.Degree ?? "",
                ExperienceYears = c.ExperienceYears ?? 0,
                bio = c.Bio ?? "",
                avatar = c.Avatar ?? ""
            }).ToList();
        }

        public async Task<ConsultantDTO?> GetByIdAsync(int id)
        {
            var consultant = await _repository.GetByIdAsync(id);
            if (consultant == null) return null;

            return new ConsultantDTO
            {
                Id = consultant.ConsultantId,
                degree = consultant.Degree ?? "",
                ExperienceYears = consultant.ExperienceYears ?? 0,
                bio = consultant.Bio ?? "",
                avatar = consultant.Avatar ?? ""
            };
        }

        public async Task<bool> CreateAsync(ConsultantDTO dto)
        {
            var entity = new Consultant
            {
                Degree = dto.degree,
                ExperienceYears = dto.ExperienceYears,
                Bio = dto.bio,
                Avatar = dto.avatar,
                UserId = dto.Id // Make sure to set actual UserId
            };

            return await _repository.CreateAsync(entity) > 0;
        }

        public async Task<bool> UpdateAsync(int id, ConsultantDTO dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Degree = dto.degree;
            existing.ExperienceYears = dto.ExperienceYears;
            existing.Bio = dto.bio;
            existing.Avatar = dto.avatar;

            return await _repository.UpdateAsync(existing) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            return await _repository.RemoveAsync(existing);
        }
    }
}
