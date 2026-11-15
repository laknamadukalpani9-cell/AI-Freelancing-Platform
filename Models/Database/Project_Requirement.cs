using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class Project_Requirement
{
    public int Requirement_ID { get; set; }

    public int Project_ID { get; set; }

    public int Skill_ID { get; set; }

    public string? Requirement_Details { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual AI_Skill Skill { get; set; } = null!;
}
