using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
       
        public DbSet<Hotel> Hotels { get; set; }

    }
}
