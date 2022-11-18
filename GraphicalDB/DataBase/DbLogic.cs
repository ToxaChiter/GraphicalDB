using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Linq;

namespace GraphicalDB.DataBase;

internal static class DbLogic
{
    private static PasswordHasher<User> PasswordHasher { get; } = new PasswordHasher<User>();
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

    public static string HashPassword(User user, string password)
    {
        return PasswordHasher.HashPassword(user, password);
    }

    public static bool CheckPassword(User user, string password)
    {
        var result = PasswordHasher.VerifyHashedPassword(user, user.Password, password);
        return result != PasswordVerificationResult.Failed;
    }

    public static void Testing()
    {
        using var context = new MyDbContext();

        context.Participants.Add(new Participant()
        {
            Name = "Test",
            BirthYear = 0
        });
        context.SaveChanges();
    }
}
