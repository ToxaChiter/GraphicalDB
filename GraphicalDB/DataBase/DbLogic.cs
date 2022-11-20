using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Security.Cryptography;
using System.Collections.ObjectModel;

namespace GraphicalDB.DataBase;

internal static class DbLogic
{
    private static string Path { get; } = "DbLog.txt";

    public static void Log(string message)
    {
        using var writer = new StreamWriter(Path, true);

        writer.WriteLine(message);
    }
    public static void AddParticipant(Participant participant)
    {
        using var context = new MyDbContext();

        context.Participants.Add(participant);
        context.SaveChanges();
        Log($"User {App.AuthorizedUser.Login} added participant {participant}");
    }
    public static void RemoveParticipant(Participant participant)
    {
        using var context = new MyDbContext();

        context.Participants.Remove(participant);
        context.SaveChanges();
        Log($"User {App.AuthorizedUser.Login} removed participant {participant}");
    }
    public static Participant? FindParticipant(Func<Participant, bool> predicate)
    {
        using var context = new MyDbContext();

        var participants = context.Participants.ToList();
        return participants.FirstOrDefault(predicate);
    }

    public static void AddUser(User user)
    {
        using var context = new MyDbContext();

        context.Users.Add(user);
        context.SaveChanges();
        Log($"User {App.AuthorizedUser.Login} added user {user}");
    }
    public static void RemoveUser(User user)
    {
        using var context = new MyDbContext();

        context.Users.Remove(user);
        context.SaveChanges();
        Log($"User {App.AuthorizedUser.Login} removed user {user}");
    }
    public static User? FindUser(Func<User, bool> predicate)
    {
        using var context = new MyDbContext();

        var users = context.Users.ToList();
        return users.FirstOrDefault(predicate);
    }

    public static string HashPassword(string password)
    {
        byte[] tmpSource;
        byte[] tmpHash;

        tmpSource = Encoding.ASCII.GetBytes(password);
        tmpHash = MD5.Create().ComputeHash(tmpSource);

        var builder = new StringBuilder(tmpHash.Length);
        for (int i = 0; i < tmpHash.Length; i++)
        {
            builder.Append(tmpHash[i]);
        }

        return builder.ToString();
    }

    public static bool CheckPassword(User user, string password)
    {
        return string.Equals(user.Password, HashPassword(password));
    }

    public static void Testing()
    {
        using var context = new MyDbContext();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();


        var user = new User()
        {
            Login = "Admin",
            Password = HashPassword("password"),
            Role = Role.Admin
        };



        var table = new ObservableCollection<Participant>
        {
            new Participant { Id = 1, Name = "Andrew", Country = "Russia", BirthYear = 2004, Instrument = Instrument.Guitar, Place = 3 },
            new Participant { Id = 2, Name = "Lila", Country = "Belarus", BirthYear = 2002, Instrument = Instrument.Piano, Place = 2 },
            new Participant { Id = 3, Name = "Ulia", Country = "Ukraine", BirthYear = 2008, Instrument = Instrument.Violin, Place = 4 },
            new Participant { Id = 4, Name = "Nick", Country = "USA", BirthYear = 1901, Instrument = Instrument.Cello, Place = 1 }
        };

        foreach (var part in table)
        {
            context.Participants.Add(part);
        }        

        context.Users.Add(user);
        context.SaveChanges();
    }
}
