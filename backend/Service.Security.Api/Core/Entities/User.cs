using Microsoft.AspNetCore.Identity;

namespace Service.Security.Api.Core.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
