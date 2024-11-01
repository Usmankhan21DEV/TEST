using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class Userbalanceandrefund
{
    public int? UserSeqNum { get; set; }

    public string? UserName { get; set; }

    public decimal? Payments { get; set; }

    public decimal? Refund { get; set; }

    public decimal? UsersExpenditure { get; set; }

    public long? UserTotalImei { get; set; }

    public decimal? CurBalance { get; set; }
}
