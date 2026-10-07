using Microsoft.EntityFrameworkCore;
using MIRANDA_Midterm_Store.Models;

namespace MIRANDA_Midterm_Store.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<CartItem> CartItems { get; set; }
}