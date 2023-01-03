using System.ComponentModel;

namespace GraphicalDB.DataBase;

enum Instruments
{
    Guitar,
    Piano,
    Violin,
    Cello
}

class Participant : INotifyPropertyChanged
{
    private string name;
    private int birthYear;
    private string country;
    private Instruments instrument;
    private int place;


    public int Id { get; set; }
    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            if (value != name)
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
    }

    public int BirthYear
    {
        get
        {
            return birthYear;
        }

        set
        {
            if (value != birthYear)
            {
                birthYear = value;
                NotifyPropertyChanged(nameof(BirthYear));
            }
        }
    }

    public string Country
    {
        get
        {
            return country;
        }

        set
        {
            if (value != country)
            {
                country = value;
                NotifyPropertyChanged(nameof(Country));
            }
        }
    }

    public Instruments Instrument
    {
        get
        {
            return instrument;
        }

        set
        {
            if (value != instrument)
            {
                instrument = value;
                NotifyPropertyChanged(nameof(Instrument));
            }
        }
    }

    public int Place
    {
        get
        {
            return place;
        }

        set
        {
            if (value != place)
            {
                place = value;
                NotifyPropertyChanged(nameof(Place));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, BirthYear: {BirthYear}, Country: {Country}, Instrument: {Instrument}, Place: {Place}";
    }
}
