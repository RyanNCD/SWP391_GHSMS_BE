using Repository.DTO;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITestService
    {
        Task<int> CreateAsync(TestDTO dto);
        Task<int> UpdateAsync(TestDTO dto);
        Task<List<TestDTO>> GetAllAsync();
        Task<TestDTO?> GetByIdAsync(int id);
    }

}
