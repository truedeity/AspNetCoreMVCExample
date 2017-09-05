using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace CoreFamework.Security
{
	/// <summary>
	/// Base class for the Entity Framework database context used for identity.
	/// </summary>
	public class SecurityContext : IdentityDbContext<User, Role, int>
	{
		/// <summary>
		/// Initializes a new instance of <see cref="IdentityDbContext"/>.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
		public SecurityContext(DbContextOptions options) : base(options) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class.
		/// </summary>
		protected SecurityContext() {

		}
	}

}