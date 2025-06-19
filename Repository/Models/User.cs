using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public int RoleId { get; set; }

    public DateTime? CreateAt { get; set; }

    public bool? IsActive { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<ConsultantUserSchedule> ConsultantUserSchedules { get; set; } = new List<ConsultantUserSchedule>();

    public virtual ICollection<Consultant> Consultants { get; set; } = new List<Consultant>();

    public virtual ICollection<ConsultationBooking> ConsultationBookings { get; set; } = new List<ConsultationBooking>();

    public virtual ICollection<Ewallet> Ewallets { get; set; } = new List<Ewallet>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<MenstrualCycle> MenstrualCycles { get; set; } = new List<MenstrualCycle>();

    public virtual ICollection<OvulationReminder> OvulationReminders { get; set; } = new List<OvulationReminder>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TestBooking> TestBookings { get; set; } = new List<TestBooking>();

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();

    public virtual ICollection<UserMessage> UserMessageReceivers { get; set; } = new List<UserMessage>();

    public virtual ICollection<UserMessage> UserMessageSenders { get; set; } = new List<UserMessage>();
}
