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
        Task<List<Feedback>> GetAllAsync();
        Task<Feedback> GetByIdAsync(int id);
        Task<int> UpdateAsync(Feedback feedback);
        Task<int> CreateAsync(Feedback feedback);
        Task<int> DeleteAsync(int id);
    }
}
