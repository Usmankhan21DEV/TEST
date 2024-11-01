using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class Aggregatedcounter
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public int Value { get; set; }

    public DateTime? ExpireAt { get; set; }
}
