﻿using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class Counter
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public int Value { get; set; }

    public DateTime? ExpireAt { get; set; }
}
