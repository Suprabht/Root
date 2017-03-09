using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationService.Configuration
{
    public class UserManager
    {
        private List<User> GetUsers()
        {
            var users = new List<User>();

            users.Add(new User { UserName = "Alice", Password = "rambo0315" });
            users.Add(new User { UserName = "Priyanka", Password = "rambo0315" });
            users.Add(new User { UserName = "Raju", Password = "rambo0315" });
            return users;
        }
        public bool CheckPasswordAsync(string userName, string password)
        {
            User user = FindByNameAsync(userName);
            if (user.Password == password)
                return true;
            return false;
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            if (user.Password == password)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }

        public User FindByNameAsync(string userName)
        {
            User user = GetUsers().SingleOrDefault(x=>x.UserName== userName);
            return user;
        }

        public Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("accountnumber", "12345"));
            return Task.FromResult(claims);
        }
    }
}
