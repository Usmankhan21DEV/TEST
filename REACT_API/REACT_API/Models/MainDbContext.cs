using Microsoft.EntityFrameworkCore;

namespace REACT_API.Models
{
    public class MainDbContext : DbContext
    {


    
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        public DbSet<user_info> user_info { get; set; }
    

}
}
