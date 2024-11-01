using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class Service
{
    public int SeqNum { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public int? ApiKey { get; set; }

    public string? Service1 { get; set; }

    public string? Type { get; set; }

    public string? Delay { get; set; }

    public virtual ICollection<PackageServiceJunction> PackageServiceJunctions { get; set; } = new List<PackageServiceJunction>();
}
