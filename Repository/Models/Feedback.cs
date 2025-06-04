using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public int? TestBookingId { get; set; }

    public int? ConsultationBookingId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual ConsultationBooking? ConsultationBooking { get; set; }

    public virtual TestBooking? TestBooking { get; set; }

    public virtual User User { get; set; } = null!;
}
