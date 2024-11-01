using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class VwUserInfo
{
    public int SeqNum { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string Address2 { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? Varified { get; set; }

    public bool? TrialPeriod { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsLogin { get; set; }

    public string? UserIp { get; set; }

    public int? RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? Rights { get; set; }

    public decimal? Balance { get; set; }

    public string? UserName { get; set; }

    public long? UserTotalImei { get; set; }

    public decimal? UsersExpenditure { get; set; }

    public decimal? TotalBalance { get; set; }
}
