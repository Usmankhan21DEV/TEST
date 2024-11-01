using System;
using System.Collections.Generic;

namespace REACT_API.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? RoleRights { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<UserInfo> UserInfos { get; set; } = new List<UserInfo>();
}
