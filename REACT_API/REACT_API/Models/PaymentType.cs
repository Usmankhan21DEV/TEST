using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class PaymentType
{
    public int SeqNum { get; set; }

    public string PaymentTypeName { get; set; } = null!;

    public DateTime EntryDate { get; set; }
}
