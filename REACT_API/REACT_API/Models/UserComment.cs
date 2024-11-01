using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class UserComment
{
    public int SeqNum { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedDate { get; set; }
}
