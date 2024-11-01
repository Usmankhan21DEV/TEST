using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class Distributedlock
{
    public string Resource { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
