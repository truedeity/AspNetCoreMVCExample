using System;
using System.Collections.Generic;

namespace AspNetCoreMVCExample.Data.Models
{
    public partial class UserClaim
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
