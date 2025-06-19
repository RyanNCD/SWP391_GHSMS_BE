using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class MenstrualCycle
{
    public int CyclesId { get; set; }

    public int UserId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int AverageLength { get; set; }

    public virtual ICollection<OvulationReminder> OvulationReminders { get; set; } = new List<OvulationReminder>();

    public virtual User User { get; set; } = null!;
}
