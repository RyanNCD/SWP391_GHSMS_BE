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

        public async Task<int> CreateAsync(TestDTO dto)
        {
            var test = new Test
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            return await _repository.CreateAsync(test);
        }

        public async Task<List<TestDTO>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(t => new TestDTO
            {
                TestId = t.TestId,
                Name = t.Name,
                Description = t.Description,
                Price = t.Price
            }).ToList();
        }

        public async Task<TestDTO?> GetByIdAsync(int id)
        {
            var test = await _repository.GetByIdAsync(id);
            if (test == null) return null;

            return new TestDTO
            {
                TestId = test.TestId,
                Name = test.Name,
                Description = test.Description,
                Price = test.Price
            };
        }

        public async Task<int> UpdateAsync(TestDTO dto)
        {
            var test = await _repository.GetByIdAsync(dto.TestId);
            if (test == null) return 0;

            test.Name = dto.Name;
            test.Description = dto.Description;
            test.Price = dto.Price;

            return await _repository.UpdateAsync(test);
        }
    }
}
