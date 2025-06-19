using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class ConsultantUserSchedule
{
    public int ScheduleId { get; set; }

    public int UserId { get; set; }

    public int ConsultantId { get; set; }

    public int? ConsultationBookingId { get; set; }

    public DateTime ScheduleDateTime { get; set; }

    public int? DurationMinutes { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual Consultant Consultant { get; set; } = null!;

    public virtual ConsultationBooking? ConsultationBooking { get; set; }

    public virtual ICollection<TestBooking> TestBookings { get; set; } = new List<TestBooking>();

    public virtual User User { get; set; } = null!;
}
