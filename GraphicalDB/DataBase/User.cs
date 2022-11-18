namespace GraphicalDB.DataBase;

enum Role
{
    Default,
    Admin
}
class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Login: {Login}, Password: {Password}, Role: {Role}";
    }
}
