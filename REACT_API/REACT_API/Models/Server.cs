using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class Server
{
    public string Id { get; set; } = null!;

    public string Data { get; set; } = null!;

    public DateTime? LastHeartbeat { get; set; }
}
