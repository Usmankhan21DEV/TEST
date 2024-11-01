using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class OrderHistory
{
    public int SeqNum { get; set; }

    public int? UserSeqNum { get; set; }

    public string? OrderId { get; set; }

    public int? InfoSeqNum { get; set; }

    public string? Imei { get; set; }

    public int? ServiceSeqNum { get; set; }

    public decimal? ServiceCost { get; set; }

    public DateTime? SearchDate { get; set; }

    public string? Status { get; set; }

    public string? ServiceName { get; set; }

    public DateTime? CompletedDate { get; set; }

    public string? RequestedEmail { get; set; }

    public virtual InfoDelay? InfoSeqNumNavigation { get; set; }

    public virtual PackageServiceJunction? ServiceSeqNumNavigation { get; set; }

    public virtual UserInfo? UserSeqNumNavigation { get; set; }
}
