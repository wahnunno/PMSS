using Microsoft.EntityFrameworkCore;

namespace Demo1.con_db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
