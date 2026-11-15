using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class AI_Service
{
    public int Service_ID { get; set; }

    public string Service_Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Freelancer> Freelancers { get; set; } = new List<Freelancer>();
}
