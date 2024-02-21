using Microsoft.EntityFrameworkCore;
using mini_pr1.Entities;


namespace mini_pr1.Data
{
    public class InputDbContext : DbContext
    {
        public InputDbContext()
        {
        }
        public InputDbContext(DbContextOptions<InputDbContext> options) : base(options)
        {

        }

        public DbSet<Schedule> Schedule { get; set; }
    }
}
