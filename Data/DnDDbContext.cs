using Microsoft.EntityFrameworkCore;
using DnDNoteKeeper.Models;

namespace DnDNoteKeeper.Data;

public class DnDDbContext : DbContext
{
    public DnDDbContext(DbContextOptions<DnDDbContext> options) : base(options)
    {
        
    }

    public DbSet<UserAccount> UserAccounts { get; set; }
}