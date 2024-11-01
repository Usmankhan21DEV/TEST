using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class InfoDelay
{
    public int IdSeq { get; set; }

    public string Imei { get; set; } = null!;

    public DateTime? EntryDate { get; set; }

    public string? IPhoneCarrierFmiBlacklist { get; set; }

    public string? ICloudOnOff { get; set; }

    public string? AppleMdmStatus { get; set; }

    public string? AppleBasicInfo { get; set; }

    public string? WwBlacklistStatus { get; set; }

    public string? UsaTMobileClean { get; set; }

    public string? UsaAtTCleanActiveLine { get; set; }

    public string? UsaCricketClean6MonthsOld { get; set; }

    public string? ClaroAllCountriesPremiumIPhone14 { get; set; }

    public string? ClaroAllCountriesPremiumIPhone15 { get; set; }

    public string? ClaroAllCountriesPremiumUpToIPhone13 { get; set; }

    public string? SamsungAtTCricketAllModels { get; set; }

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
}
