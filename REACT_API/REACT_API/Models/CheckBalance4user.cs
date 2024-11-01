using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class CheckBalance4user
{
    public int? UserSeqNum { get; set; }

    public decimal? Balance { get; set; }

    public string FullName { get; set; } = null!;
}
