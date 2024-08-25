using Microsoft.AspNetCore.Identity;

namespace WebHulk.Data.Entities.Identity
{
    public class RoleEntity : IdentityRole<int>
    {
        public ICollection<UserRoleEntity> Roles { get; set; } = [];
    }
}
