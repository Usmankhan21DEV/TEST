using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class Refund
{
    public int SeqNum { get; set; }

    public decimal Amount { get; set; }

    public int? ServiceNo { get; set; }

    public int? PackageSeqNum { get; set; }

    public int? BalanceSeqNum { get; set; }

    public int UserSeqNum { get; set; }

    public string? Reason { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual UserBalance? BalanceSeqNumNavigation { get; set; }

    public virtual Package? PackageSeqNumNavigation { get; set; }

    public virtual PackageServiceJunction? ServiceNoNavigation { get; set; }

    public virtual UserInfo UserSeqNumNavigation { get; set; } = null!;
}
