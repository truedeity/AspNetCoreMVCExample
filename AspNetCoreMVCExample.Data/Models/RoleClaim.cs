using System;
using System.Collections.Generic;

namespace AspNetCoreMVCExample.Data.Models
{
    public partial class RoleClaim
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
