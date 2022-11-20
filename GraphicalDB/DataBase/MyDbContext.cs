using Microsoft.EntityFrameworkCore;

namespace GraphicalDB.DataBase;

internal class MyDbContext : DbContext
{
    public MyDbContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public DbSet<Participant> Participants { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=MusicalContest.db");
    }
}
