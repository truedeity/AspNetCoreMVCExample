using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreFamework.Security
{
    public partial class RoleClaim : IdentityRoleClaim<int>
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
