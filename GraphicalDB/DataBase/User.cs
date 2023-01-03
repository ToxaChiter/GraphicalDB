using System.ComponentModel;

namespace GraphicalDB.DataBase;

enum Roles
{
    Default,
    Admin
}
class User : INotifyPropertyChanged
{
    private string login;
    private string password;
    private Roles role;


    public int Id { get; set; }
    public string Login 
    { 
        get
        {
            return login;
        }

        set
        {
            if (value != login)
            {
                login = value;
                NotifyPropertyChanged(nameof(Login));
            }
        }
    }
    public string Password 
    { 
        get
        {
            return password;
        }
        set
        {
            if (value != password)
            {
                password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }
    }
    public Roles Role 
    { 
        get
        {
            return role;
        }
        set
        {
            if (value != role)
            {
                role = value;
                NotifyPropertyChanged(nameof(Role));
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
        return $"Id: {Id}, Login: {Login}, Password: {Password}, Role: {Role}";
    }
}
