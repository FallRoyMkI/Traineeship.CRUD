using Microsoft.EntityFrameworkCore;
using WorkTest.Dal.Models;

namespace WorkTest.Dal;

public class Context : DbContext
{
    public DbSet<OrderDto> Orders { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) { }

    //protected override void OnConfiguring(DbContextOptionsBuilder builder)
    //{
    //    builder.UseNpgsql(@"Server=127.0.0.1;Port=5432;Database=Test;User Id=postgres;Password=263227;");
    //}
}