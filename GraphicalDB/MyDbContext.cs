using Microsoft.EntityFrameworkCore;

namespace GraphicalDB;

internal class MyDbContext : DbContext
{
    public MyDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public DbSet<Participant> Participants { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=MusicalContest.db");
        optionsBuilder.LogTo(DbLogic.Log, Microsoft.Extensions.Logging.LogLevel.Information);
    }
}
