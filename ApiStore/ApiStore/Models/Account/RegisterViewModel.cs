namespace ApiStore.Models.Account;
public class RegisterViewModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public IFormFile? Image { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
