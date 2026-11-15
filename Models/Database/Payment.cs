using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class Payment
{
    public int Payment_ID { get; set; }

    public int Assignment_ID { get; set; }

    public decimal Amount { get; set; }

    public DateOnly? Payment_Date { get; set; }

    public string? Payment_Status { get; set; }

    public virtual Assignment Assignment { get; set; } = null!;
}
