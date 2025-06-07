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
        Task<List<Test>> GetAllAsync();
        Task<Test> GetByIdAsync(int UserId);
        Task<int> UpdateAsync(Test test);
        Task<int> CreateAsync(Test test);
    }
}
