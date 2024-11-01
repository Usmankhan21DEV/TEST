using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class Package
{
    public int SeqNum { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<PackageServiceJunction> PackageServiceJunctions { get; set; } = new List<PackageServiceJunction>();

    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
}
