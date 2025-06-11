using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Test
{
    public int TestId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<TestBooking> TestBookings { get; set; } = new List<TestBooking>();

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}
