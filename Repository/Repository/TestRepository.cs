using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.DTO;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
   public class TestRepository : GenericRepository<Test>
    {
        public TestRepository() { }
        public TestRepository(SWP391GHSMContext context) => _context = context;

        public async Task<bool> CreateTest(TestDTO request)
        {
            try
            {
                var getConsutant = await _context.Consultants.FirstOrDefaultAsync(x => x.UserId == request.consultantId);
                var createNewTest = new Test
                {
                    TestId = Guid.NewGuid(),
                    Name = request.TestName,
                    Description = request.Description,
                    Price = request.Price,
                    Date = request.Date,
                    isBooked = false,
                    isDelete = false
                };


                await _context.Tests.AddAsync(createNewTest);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<TestResponse>> GetTestsByConsutant(Guid ConsutantId)
        {
            var getTests = await _context.Tests.Where(x => x.isDelete == false).ToListAsync();

            var mapItem = getTests.Select(c => new TestResponse
            {
                TestId = c.TestId,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                Date = c.Date,
                IsBooked = c.isBooked,
                IsDelete = c.isDelete
            }).ToList();

            return mapItem;
        }

        public async Task<TestDetailResponse> GetTestDetail(Guid testId)
        {
            var getDetail = await _context.Tests
                                          .FirstOrDefaultAsync(x => x.TestId == testId);
            if (getDetail == null)
            {
                return null;
            }

            var response = new TestDetailResponse
            {
                Test = new TestResponse
                {
                    TestId = getDetail.TestId,
                    Name = getDetail.Name,
                    Description = getDetail.Description,
                    Price = getDetail.Price,
                    Date = getDetail.Date,
                    IsDelete = getDetail.isDelete,
                    IsBooked = getDetail.isBooked
                }
            };

            return response;
        }
    }
}
