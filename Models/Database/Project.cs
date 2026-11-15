using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class Project
{
    public int Project_ID { get; set; }

    public int Client_ID { get; set; }

    public string Project_Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Budget { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Project_Requirement> Project_Requirements { get; set; } = new List<Project_Requirement>();

    public virtual ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();
}
