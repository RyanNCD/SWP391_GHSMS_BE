using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Consultant
{
    public int ConsultantId { get; set; }

    public int UserId { get; set; }

    public string? Degree { get; set; }

    public int? ExperienceYears { get; set; }

    public string? Bio { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<ConsultantUserSchedule> ConsultantUserSchedules { get; set; } = new List<ConsultantUserSchedule>();

    public virtual ICollection<ConsultationBooking> ConsultationBookings { get; set; } = new List<ConsultationBooking>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual User User { get; set; } = null!;
}
