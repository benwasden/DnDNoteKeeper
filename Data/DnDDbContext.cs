using Microsoft.EntityFrameworkCore;
using DnDNoteKeeper.Models;

namespace DnDNoteKeeper.Data;

public class DnDDbContext : DbContext
{
    // This constructor is required so Program.cs can pass the Connection String into it
    public DnDDbContext(DbContextOptions<DnDDbContext> options) 
        : base(options) 
    { 
    }

    // Each DbSet represents a table in your SQL Database
    public DbSet<CampaignNote> Notes { get; set; } = default!;
}