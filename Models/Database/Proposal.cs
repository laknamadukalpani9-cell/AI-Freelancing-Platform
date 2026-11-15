using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class Proposal
{
    public int Proposal_ID { get; set; }

    public int Project_ID { get; set; }

    public int Freelancer_ID { get; set; }

    public decimal Proposed_Amount { get; set; }

    public string? Proposal_Status { get; set; }

    public virtual Freelancer Freelancer { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
