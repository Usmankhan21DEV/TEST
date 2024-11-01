using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class ImeiUserJunction
{
    public int SeqNum { get; set; }

    public int? UserSeqNum { get; set; }

    public int? InfoSeqNum { get; set; }

    public int? InfoDSeqNum { get; set; }

    public int? ServiceNo { get; set; }

    public decimal? ServiceFee { get; set; }

    public DateTime? SearchDate { get; set; }

    public virtual Info? InfoSeqNumNavigation { get; set; }

    public virtual UserInfo? UserSeqNumNavigation { get; set; }
}
