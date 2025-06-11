using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class OvulationReminder
{
    public int ReminderId { get; set; }

    public int UserId { get; set; }

    public string Type { get; set; } = null!;

    public DateTime ReminderDate { get; set; }

    public virtual User User { get; set; } = null!;
}
