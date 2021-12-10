namespace ZIT.Core.DTOs;

public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; }
    public List<string> Entitlements { get; set; }
}