using Microsoft.EntityFrameworkCore;

namespace AOEntityFramework
{

    public class AODbContext : DbContext
    {
        public AODbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
