using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class ViewUserbalance
{
    public int SeqNum { get; set; }

    public int UserSeqNum { get; set; }

    public decimal? Balance { get; set; }

    public string PackageName { get; set; } = null!;

    public string UserName { get; set; } = null!;
}
