using ApiStore.Data.Entities.Identity;

namespace ApiStore.Interfaces;

public interface IJwtTokenService
{
    public string GenerateToken(UserEntity user);
}