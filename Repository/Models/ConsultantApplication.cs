using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class ConsultantApplication
{
    public int ApplicationId { get; set; }

    public int UserId { get; set; }

    public string? Degree { get; set; }

    public int? ExperienceYears { get; set; }

    public string? Bio { get; set; }

    public string? Status { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public int? ReviewedBy { get; set; }

    public virtual User? ReviewedByNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
