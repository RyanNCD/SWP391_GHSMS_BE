using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class DashBoardRepository 
    {
        private readonly SWP391GHSMContext _context;

        public DashBoardRepository(SWP391GHSMContext context)
        {
            _context = context;
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetTestCountAsync()
        {
            return await _context.Tests.CountAsync(); // hoặc TestSessions
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Tests.SumAsync(t => (decimal?)t.Price) ?? 0;
        }

        public async Task<double> GetAverageFeedbackAsync()
        {
            return await _context.Feedbacks.AverageAsync(f => (double?)f.Rating) ?? 0;
        }
    }

}
