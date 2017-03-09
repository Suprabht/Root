using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;
using IdentityServer4;

namespace AuthorizationService.Configuration
{
    public class Scopes
    {
        public static List<Scope> GetScopes()
        {
            return new List<Scope>
            {
                //StandardScopes.OpenId,
                //StandardScopes.Profile,

                new Scope
                {
                    Name = "api1",
                    DisplayName = "API 1",
                    Description = "API 1 features and data"
                  //  Type = ScopeType.Resource
                }
            };
        }
    }
}
