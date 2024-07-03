public class Smtpsettings
{
    public string SmtpServer { get; set; }
    public int? Port { get; set; } // Make Port nullable
    public string Username { get; set; }
    public string Password { get; set; }
}
