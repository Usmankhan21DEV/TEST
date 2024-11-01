using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class ActivityLog
{
    public int SeqNum { get; set; }

    public string TableSeqNum { get; set; } = null!;

    public string FromUser { get; set; } = null!;

    public string UserSeqNum { get; set; } = null!;

    public string Activity { get; set; } = null!;

    public string CreatedDate { get; set; } = null!;
}
