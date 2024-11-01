using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class VwUserPackage
{
    public int SeqNum { get; set; }

    public string Name { get; set; } = null!;

    public int? UserSeqNum { get; set; }
}
