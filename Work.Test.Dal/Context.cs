using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WorkTest.Models.Entity;

namespace WorkTest.Dal;

public class Context : DbContext
{
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderLineEntity> OrderLines { get; set; }
    public Context(DbContextOptions<Context> options) : base(options) { }
}