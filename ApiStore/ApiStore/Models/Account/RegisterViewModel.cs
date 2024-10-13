using Microsoft.AspNetCore.Mvc;

namespace ApiStore.Models.Account;
public class RegisterViewModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Image { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
