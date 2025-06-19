using Repository.Base;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FeedbackRepository : GenericRepository<Feedback>
    {
        public FeedbackRepository() { }
        public FeedbackRepository(SWP391GHSMContext context) => _context = context;
    }
}
