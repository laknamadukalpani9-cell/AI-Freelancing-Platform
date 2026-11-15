using System;
using System.Collections.Generic;

namespace AIFreelancingPlatform.Models.Database;

public partial class vw_ProjectDetail
{
    public int Project_ID { get; set; }

    public string Project_Name { get; set; } = null!;

    public string Client_Name { get; set; } = null!;

    public decimal? Budget { get; set; }

    public string? Status { get; set; }
}
