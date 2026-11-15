using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class vw_FreelancerDetail
{
    public int Freelancer_ID { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Location { get; set; }
}
