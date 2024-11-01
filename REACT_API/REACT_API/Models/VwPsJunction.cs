using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class VwPsJunction
{
    public int SeqNum { get; set; }

    public string? PackageName { get; set; }

    public string? ServiceName { get; set; }

    public decimal Cost { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int PackageSeqNum { get; set; }

    public int ServiceSeqNum { get; set; }
}
