using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class VwRefund
{
    public int SeqNum { get; set; }

    public string? UserName { get; set; }

    public int UserSeqNum { get; set; }

    public decimal Amount { get; set; }

    public int? PackageSeqNum { get; set; }

    public string? ServiceNo { get; set; }

    public string? Name { get; set; }

    public int? BalanceSeqNum { get; set; }

    public string? Reason { get; set; }

    public DateTime? CreatedDate { get; set; }
}
