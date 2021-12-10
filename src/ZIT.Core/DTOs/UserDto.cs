﻿namespace ZIT.Core.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public List<string> Entitlements { get; set; }
}