using Repository.DTO;
using Repository.Models;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> AddTest(TestDTO test)
        {
            return await _repository.CreateTest(test);
        }

        public async Task<int> CreateAsync(Test test)
        {
            return await _repository.CreateAsync(test);
        }

        public Task<List<Test>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public async Task<Test> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<TestResponse>> GetTestByConsutant(Guid ConsutantId)
        {
            return await _repository.GetTestsByConsutant(ConsutantId);
        }

        public async Task<TestDetailResponse> GetTestDetailAsync(Guid TestId)
        {
            return await _repository.GetTestDetail(TestId);
        }

        public async Task<int> UpdateAsync(Test test)
        {
            return await _repository.UpdateAsync(test);
        }
    }
}
