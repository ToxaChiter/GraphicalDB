using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphicalDB;

internal static class DbLogic
{
    private static string Path { get; } = "DbLog.txt";

    public static void Connectdatabase()
    {
        using var context = new MyDbContext();

        context.Database.EnsureCreated();
    }

    public static void Log(string message)
    {
        using var writer = new StreamWriter(Path);

        writer.WriteLine(message);
    }
    public static void AddParticipant(Participant participant)
    {
        using var context = new MyDbContext();

        context.Participants.Add(participant);
        context.SaveChanges();
    }
    public static void RemoveParticipant(Participant participant)
    {
        using var context = new MyDbContext();

        context.Participants.Remove(participant);
        context.SaveChanges();
    }
    public static Participant? FindParticipant(Func<Participant, bool> predicate)
    {
        using var context = new MyDbContext();

        var participants = context.Participants.ToList();
        return participants.FirstOrDefault(predicate);
    }
}
