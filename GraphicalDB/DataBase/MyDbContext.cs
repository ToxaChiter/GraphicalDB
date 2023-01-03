using Microsoft.EntityFrameworkCore;

namespace GraphicalDB.DataBase;

internal class MyDbContext : DbContext
{
    public MyDbContext()
    {
        // Если БД не существует, создаём
        Database.EnsureCreated();
    }
    // Коллекция участников конкурса
    public DbSet<Participant> Participants { get; set; } = null!;
    // Коллекция пользователей
    public DbSet<User> Users { get; set; } = null!;

    // Метод настройки подключения к БД
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=MusicalContest.db");
    }
}
