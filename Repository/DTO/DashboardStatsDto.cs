using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class DashboardStatsDto
    {
        public int UserCount { get; set; }
        public int TestCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public double AverageFeedback { get; set; }
    }

}
