using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class Freelancer
{
    public int Freelancer_ID { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();

    public virtual ICollection<AI_Service> Services { get; set; } = new List<AI_Service>();

    public virtual ICollection<AI_Skill> Skills { get; set; } = new List<AI_Skill>();
}
