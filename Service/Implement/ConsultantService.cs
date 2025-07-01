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
    public class ConsultantsService(ConsultantsRepository _consultantRepository) : IConsultantsService
    {
        public async Task<bool> CreateConsultants(CreateConsultantsDTO request)
        {
            return await _consultantRepository.CreateConsultants(request);
        }

        public async Task<bool> DeleteConsultants(Guid consultansId)
        {
            return await _consultantRepository.DeleteConsultantDetail(consultansId);
        }

        public async Task<bool> EditConsultantDetail(Guid consultansId, CreateConsultantsDTO request)
        {
            return await _consultantRepository.EditConsultantDetail(consultansId, request);
        }

        public async Task<GetConsultantDetail> GetConsultantDetail(Guid consultansId)
        {
            return await _consultantRepository.GetConsultantDetail(consultansId);
        }

        public async Task<List<GetConsultants>> GetConsultants()
        {
            return await _consultantRepository.GetConsultants();
        }
    }
}
