using Microsoft.EntityFrameworkCore;
using MyBooks.Data.Entities;

namespace MyBooks.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
