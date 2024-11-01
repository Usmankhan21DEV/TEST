using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class UserBalance
{
    public int SeqNum { get; set; }

    public int? UserSeqNum { get; set; }

    public decimal? Balance { get; set; }

    public int? PackageSeqNum { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual UserInfo? UserSeqNumNavigation { get; set; }
}
