using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class AI_Skill
{
    public int Skill_ID { get; set; }

    public string Skill_Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Project_Requirement> Project_Requirements { get; set; } = new List<Project_Requirement>();

    public virtual ICollection<Freelancer> Freelancers { get; set; } = new List<Freelancer>();
}
