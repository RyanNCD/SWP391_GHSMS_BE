using Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IConsultantService
    {
        Task<List<ConsultantDTO>> GetAllAsync();
        Task<ConsultantDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ConsultantDTO dto);
        Task<bool> UpdateAsync(int id, ConsultantDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
