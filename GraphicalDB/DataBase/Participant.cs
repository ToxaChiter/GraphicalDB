namespace GraphicalDB.DataBase;

enum Instrument
{
    Guitar,
    Piano,
    Violin,
    Cello
}

class Participant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BirthYear { get; set; }
    public string Country { get; set; }
    public Instrument Instrument { get; set; }
    public int Place { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, BirthYear: {BirthYear}, Country: {Country}, Instrument: {Instrument}, Place: {Place}";
    }
}
