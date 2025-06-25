using Repository.DTO;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFeedbackService
    {
        Task<List<FeedbackDTO>> GetAllAsync();
        Task<FeedbackDTO> GetByIdAsync(int id);
        Task<int> UpdateAsync(FeedbackDTO feedback);
        Task<int> CreateAsync(FeedbackDTO feedback);
        Task<int> DeleteAsync(int id);
    }
}
