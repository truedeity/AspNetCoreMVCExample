using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CoreFamework.Security
{
    public partial class Role : IdentityRole<int>
	{
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public bool? IsSuppressed { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? UpdatedDt { get; set; }
        public int? UpdatedByUserId { get; set; }
        public Guid LastUpdateGuid { get; set; }
        public Guid EntityGuid { get; set; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}
