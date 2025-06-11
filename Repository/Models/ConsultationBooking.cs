using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class ConsultationBooking
{
    public int ConsultationBookingId { get; set; }

    public int UserId { get; set; }

    public int ConsultantId { get; set; }

    public DateTime Datetime { get; set; }

    public string? Title { get; set; }

    public string? LinkConsultation { get; set; }

    public string? Status { get; set; }

    public virtual Consultant Consultant { get; set; } = null!;

    public virtual ICollection<ConsultantUserSchedule> ConsultantUserSchedules { get; set; } = new List<ConsultantUserSchedule>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User User { get; set; } = null!;
}
