using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class BackupImeiIpc
{
    public int SeqNum { get; set; }

    public string Imei { get; set; } = null!;

    public DateTime? EntryDate { get; set; }

    public string? IphoneCarrier { get; set; }
}
