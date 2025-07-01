using Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IConsultantsService
    {
        Task<bool> CreateConsultants(CreateConsultantsDTO request);
        Task<List<GetConsultants>> GetConsultants();
        Task<GetConsultantDetail> GetConsultantDetail(Guid consultansId);
        Task<bool> DeleteConsultants(Guid consultansId);
        Task<bool> EditConsultantDetail(Guid consultansId, CreateConsultantsDTO request);
    }
}
