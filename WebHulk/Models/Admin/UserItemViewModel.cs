namespace WebHulk.Models.Admin
{
    public class UserItemViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
