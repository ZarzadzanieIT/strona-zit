namespace ZIT.Core.DTOs;

public class AddUserDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; }
    public List<string> Entitlements { get; set; }
}