using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class VwUserbalanceNew4mat
{
    public string? UserName { get; set; }

    public string? Email { get; set; }

    public decimal? Gold { get; set; }

    public decimal? Silver { get; set; }

    public decimal? Bronze { get; set; }

    public decimal Total { get; set; }
}
