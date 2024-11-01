using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class UserInfo
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

    public string ZipPostalCode { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Token { get; set; }

    public bool? Varified { get; set; }

    public bool? TrailPeriod { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsLogin { get; set; }

    public string? UserIp { get; set; }

    public string? UserBrowser { get; set; }

    public string? UserHostName { get; set; }

    public string? DeviceType { get; set; }

    public int? Roles { get; set; }

    public virtual ICollection<ImeiUserJunction> ImeiUserJunctions { get; set; } = new List<ImeiUserJunction>();

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual UserRole? RolesNavigation { get; set; }

    public virtual ICollection<UserBalance> UserBalances { get; set; } = new List<UserBalance>();
}
