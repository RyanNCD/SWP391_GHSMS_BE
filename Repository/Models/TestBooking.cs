using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class TestBooking
{
    public int TestBookingId { get; set; }

    public int UserId { get; set; }

    public int TestId { get; set; }

    public DateTime ScheduledDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Test Test { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
