using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class VwUserServiceCost
{
    public int? SeqNum { get; set; }

    public int? UserSeqNum { get; set; }

    public decimal? Balance { get; set; }

    public string? Packages { get; set; }

    public int? PackageSeqNum { get; set; }

    public string? Services { get; set; }

    public int? ServiceSeqNum { get; set; }

    public int? PsjSeqNum { get; set; }

    public decimal? Cost { get; set; }

    public DateTime? CostCreatedDate { get; set; }

    public DateTime? BalanceCreatedDate { get; set; }

    public string? CompleteServiceName { get; set; }
}
