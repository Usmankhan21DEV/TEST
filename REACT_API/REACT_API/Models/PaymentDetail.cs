using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class PaymentDetail
{
    public int SeqNum { get; set; }

    public string? TransId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Status { get; set; }

    public int? UserSeqNum { get; set; }

    public string? BuyerEmail { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? BuyerId { get; set; }

    public decimal Amount { get; set; }

    public int? PackageSeqNum { get; set; }

    public string? Tax { get; set; }

    public DateTime? CreateDatetime { get; set; }

    public DateTime? UpdateDatetime { get; set; }

    public virtual Package? PackageSeqNumNavigation { get; set; }

    public virtual UserInfo? UserSeqNumNavigation { get; set; }
}
