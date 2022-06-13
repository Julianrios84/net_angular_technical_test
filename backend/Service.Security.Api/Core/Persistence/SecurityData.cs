using Microsoft.AspNetCore.Identity;
using Service.Security.Api.Core.Entities;

namespace Service.Security.Api.Core.Persistence
{
    public class SecurityData
    {
        public static async Task InsertUser(SecurityContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    FirstName = "Administrator",
                    LastName = "Default",
                    UserName = "administrator",
                    Email = "default@administrator"
                };

                await userManager.CreateAsync(user, "Administrator123$");
            }
        }
    }
}
