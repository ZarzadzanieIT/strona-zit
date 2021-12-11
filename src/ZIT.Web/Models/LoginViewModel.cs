using ZIT.Core.DTOs;

namespace ZIT.Web.Models;

public class LoginViewModel
{
    public LoginDto Login { get; set; }
    public string[] Messages { get; set; }

    public LoginViewModel(LoginDto login, params string[] messages)
    {
        Login = login;
        Messages = messages;
    }

    public LoginViewModel(LoginDto login)
    {
        Login = login;
        Messages = Array.Empty<string>();
    }

    public LoginViewModel()
    {
        Login = new LoginDto();
        Messages = Array.Empty<string>();
    }
}