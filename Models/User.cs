namespace api_gestion_productos.Models;
public class User
{
    public int id { get; set; }
    public string name { get; set; } = "";
    public string lastName { get; set; } = "";
    public int age { get; set; }
    public string email { get; set; } = "";
    public string telephone { get; set; } = "";
    public string password { get; set; } = "";
}

