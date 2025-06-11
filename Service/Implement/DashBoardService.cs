using Repository.DTO;
using Repository.Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class DashBoardService : IDashBoardService
    {
        private readonly DashBoardRepository _repo;

        public DashBoardService(DashBoardRepository repo)
        {
            _repo = repo;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var userCount = await _repo.GetUserCountAsync();
            var testCount = await _repo.GetTestCountAsync();
            var totalRevenue = await _repo.GetTotalRevenueAsync();
            var averageFeedback = await _repo.GetAverageFeedbackAsync();

            return new DashboardStatsDto
            {
                UserCount = userCount,
                TestCount = testCount,
                TotalRevenue = totalRevenue,
                AverageFeedback = Math.Round(averageFeedback, 2)
            };
        }
    }

}
