namespace NotesApp.Identity.Models;

public class DBConnectionDetails
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string DataBase { get; set; }
    public string AuthDataBase { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string AuthMechanism { get; set; }
    public bool UserTls { get; set; }
}
