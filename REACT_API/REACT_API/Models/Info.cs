using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class Info
{
    public int IdSeq { get; set; }

    public string Imei { get; set; } = null!;

    public DateTime? EntryDate { get; set; }

    public string? IphoneCarrier { get; set; }

    public string? AppleMdmStatus { get; set; }

    public string? IPhoneSimLock { get; set; }

    public string? AppleActivationStatusImeiSn { get; set; }

    public string? MdmLockBypassIPhoneIPad { get; set; }

    public string? OneplusInfo { get; set; }

    public string? MotorolaInfo { get; set; }

    public string? GooglePixelInfo { get; set; }

    public string? SamsungInfo { get; set; }

    public string? SamsungInfoPro { get; set; }

    public string? BrandModelInfo { get; set; }

    public virtual ICollection<ImeiUserJunction> ImeiUserJunctions { get; set; } = new List<ImeiUserJunction>();
}
