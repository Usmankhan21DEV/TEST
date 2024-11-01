using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class PackageServiceJunction
{
    public int SeqNum { get; set; }

    public int ServiceSeqNum { get; set; }

    public int PackageSeqNum { get; set; }

    public decimal Cost { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual Package PackageSeqNumNavigation { get; set; } = null!;

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual Service ServiceSeqNumNavigation { get; set; } = null!;
}
