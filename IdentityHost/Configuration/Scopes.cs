using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityHost.Configuration
{
    public class Scopes
    {

		public static List<Scope> GetScopes()
		{
			return new List<Scope>() {
				new Scope() {
					Name = "myApi",
					Description = "Some desc"
				}
			};
		}
    }
}
