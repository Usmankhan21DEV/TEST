using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class VwUserImei
{
    public int SeqNum { get; set; }

    public int? UserSeqNum { get; set; }

    public string? FullName { get; set; }

    public int? InfoSeqNum { get; set; }

    public int? InfoDSeqNum { get; set; }

    public int? ServiceNo { get; set; }

    public int? ServiceSeqNum { get; set; }

    public string? PackageName { get; set; }

    public string? ServiceName { get; set; }

    public decimal? ServiceFee { get; set; }

    public long? IdSeq { get; set; }

    public string? Imei { get; set; }

    public string? IphoneCarrier { get; set; }

    public string? IPhoneCarrierFmiBlacklist { get; set; }

    public string? ICloudOnOff { get; set; }

    public string? AppleMdmStatus { get; set; }

    public string? IPhoneSimLock { get; set; }

    public string? AppleBasicInfo { get; set; }

    public DateTime? EntryDate { get; set; }

    public DateTime? SearchDate { get; set; }

    public decimal? TotalCost { get; set; }

    public string? AppleActivationStatusImeiSn { get; set; }

    public string? MdmLockBypassIPhoneIPad { get; set; }

    public string? WwBlacklistStatus { get; set; }

    public string? UsaTMobileClean { get; set; }

    public string? UsaAtTCleanActiveLine { get; set; }

    public string? UsaCricketClean6MonthsOld { get; set; }

    public string? ClaroAllCountriesPremiumIPhone14 { get; set; }

    public string? ClaroAllCountriesPremiumIPhone15 { get; set; }

    public string? ClaroAllCountriesPremiumUpToIPhone13 { get; set; }

    public string? SamsungAtTCricketAllModels { get; set; }

    public string? OneplusInfo { get; set; }

    public string? MotorolaInfo { get; set; }

    public string? GooglePixelInfo { get; set; }

    public string? SamsungInfo { get; set; }

    public string? SamsungInfoPro { get; set; }

    public string? BrandModelInfo { get; set; }
}
