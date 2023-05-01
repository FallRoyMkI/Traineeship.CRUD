using Microsoft.EntityFrameworkCore;
using WorkTest.Models.Entity;

namespace WorkTest.Dal;

public class Context : DbContext
{
    public DbSet<OrderEntity> Orders { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) { }
}