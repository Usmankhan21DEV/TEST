using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class VwOrderProgress
{
    public long Completed { get; set; }

    public long Rejected { get; set; }

    public long Pending { get; set; }

    public long Total { get; set; }

    public int? UserSeqNum { get; set; }
}
