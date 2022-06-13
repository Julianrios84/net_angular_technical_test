using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service.Security.Api.Core.Entities;

namespace Service.Security.Api.Core.Persistence
{
    public class SecurityContext: IdentityDbContext<User>
    {
        public SecurityContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
