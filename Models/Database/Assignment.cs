using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class Assignment
{
    public int Assignment_ID { get; set; }

    public int Project_ID { get; set; }

    public int Freelancer_ID { get; set; }

    public DateOnly? Start_Date { get; set; }

    public DateOnly? End_Date { get; set; }

    public virtual Freelancer Freelancer { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
