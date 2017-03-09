using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationService.Configuration
{
    public class ProfileService : IProfileService
    {
        UserManager userManager;
        public ProfileService()
        {
            userManager = new UserManager();
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.FindFirst("sub")?.Value;
            if (sub != null)
            {
                User user =  userManager.FindByNameAsync(sub);
                var cp = await getClaims(user);

                var claims = cp.Claims;
                if (context.RequestedClaimTypes != null && context.RequestedClaimTypes.Any())
                {
                    claims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToArray().AsEnumerable();
                }

              //  context.IssuedClaims = claims;
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }

        private async Task<ClaimsPrincipal> getClaims(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var id = new ClaimsIdentity();
            id.AddClaim(new Claim(JwtClaimTypes.PreferredUserName, user.UserName));

            id.AddClaims(await userManager.GetClaimsAsync(user));

            return new ClaimsPrincipal(id);
        }
    }
}
