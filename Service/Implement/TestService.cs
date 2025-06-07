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
    public class TestService : ITestService
    {
        private readonly TestRepository _repository;
        public TestService(TestRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> CreateAsync(Test test)
        {
            return await _repository.CreateAsync(test);
        }

        public Task<List<Test>> GetAllAsync()
        {
            return  _repository.GetAllAsync();
        }

        public async Task<Test> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(Test test)
        {
           return await _repository.UpdateAsync(test);
        }
    }
}
