using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class Review
{
    public int Review_ID { get; set; }

    public int Assignment_ID { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateOnly? Review_Date { get; set; }

    public virtual Assignment Assignment { get; set; } = null!;
}
