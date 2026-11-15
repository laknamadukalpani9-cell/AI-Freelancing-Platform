using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class Client
{
    public int Client_ID { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Company_Name { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
