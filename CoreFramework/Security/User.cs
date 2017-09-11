using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFamework.Security
{
    public partial class User : IdentityUser<int>
	{
		public User() 
        {
            UserLogin = new HashSet<UserLogin>();
            UserRole = new HashSet<UserRole>();

		}

		[Column("UserId")]
        public new int Id { get; set; }
        public bool IsSuppressed { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? UpdatedDt { get; set; }
        public int? UpdatedByUserId { get; set; }
        public Guid LastUpdateGuid { get; set; }
        public Guid EntityGuid { get; set; }

        public ICollection<UserLogin> UserLogin { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
    }
}
