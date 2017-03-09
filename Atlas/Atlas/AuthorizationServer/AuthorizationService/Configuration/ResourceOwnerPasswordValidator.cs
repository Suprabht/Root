using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private UserManager userManager { get; set;}
        public ResourceOwnerPasswordValidator()
        {
            userManager = new UserManager();
        }
        
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (context.UserName != null && userManager.CheckPasswordAsync(context.UserName, context.Password))
            {
                context.Result = new GrantValidationResult(context.UserName, "password");
                return Task.FromResult(0);
            }

            return Task.FromResult(new GrantValidationResult(TokenRequestErrors.UnauthorizedClient));
        }
        
    }
}
