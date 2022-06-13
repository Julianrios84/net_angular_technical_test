using Microsoft.EntityFrameworkCore;
using Service.Categories.Api.Core.Entities;

namespace Service.Categories.Api.Core.Persistence
{
    public class CategoryContext : DbContext
    {
        public CategoryContext() { }
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options) { }
        public virtual DbSet<Category> Categories { get; set; }

    }
}
