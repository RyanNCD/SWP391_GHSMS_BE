using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Base;
using Repository.DTO;
using Repository.Models;

namespace Repository.Repository
{
    public class ConsultantUserScheduleRepository(SWP391GHSMContext _context)
    {
        //public async Task<bool> Create(CreateConsultantUserSchedule request)
        //{
        //    try
        //    {
        //        var create = new ConsultantUserSchedule
        //        {
        //            ScheduleId = Guid.NewGuid(),
        //            ConsultantId = request.ConsutantId,
        //            ScheduleDateTime = request.scheduleDateTime,
        //            DurationMinutes = request.durationMinutes,
        //            Status = "PENDING",
        //            Notes = request.notes,
        //            UserId = request.userId,

        //        };

        //        await _context.ConsultantUserSchedules.AddAsync(create);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //}
    }
}
