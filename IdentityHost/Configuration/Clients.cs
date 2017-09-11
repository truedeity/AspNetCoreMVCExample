using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityHost.Configuration
{
    public class Clients
    {
		public static List<Client> ClientsList()
		{
			return new List<Client>() {
				new Client() {
					ClientId = "resClient",
					ClientSecrets = new List<Secret>() {
						new Secret("topsecrete".Sha256())
					},
					AllowedScopes = new List<string>() {
						"myApi"
					},
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword
				}
			};

		}
    }
}
