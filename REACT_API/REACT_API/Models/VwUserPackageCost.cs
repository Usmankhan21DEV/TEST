using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class VwUserPackageCost
{
    public int SeqNum { get; set; }

    public int? UserSeqNum { get; set; }

    public decimal? Balance { get; set; }

    public int? PackageSeqNum { get; set; }

    public int? ServiceSeqNum { get; set; }

    public int? PsjSeqNum { get; set; }

    public decimal? Cost { get; set; }

    public DateTime? BalanceCreateDate { get; set; }
}
