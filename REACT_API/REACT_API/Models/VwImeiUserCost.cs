using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class VwImeiUserCost
{
    public int SeqNum { get; set; }

    public int? InfoSeqNum { get; set; }

    public int? UserSeqNum { get; set; }

    public int ServiceNo { get; set; }

    public int ServiceSeqNum { get; set; }

    public int PackageSeqNum { get; set; }

    public decimal ServiceFee { get; set; }

    public DateTime? SearchDate { get; set; }

    public DateTime? PsCreatedDate { get; set; }
}
