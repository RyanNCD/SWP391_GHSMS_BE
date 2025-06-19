using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class OvulationReminder
{
    public int ReminderId { get; set; }

    public int UserId { get; set; }

    public int CyclesId { get; set; }

    public DateOnly ReminderDate { get; set; }

    public string? Type { get; set; }

    public string? Note { get; set; }

    public int? CycleDay { get; set; }

    public virtual MenstrualCycle Cycles { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
