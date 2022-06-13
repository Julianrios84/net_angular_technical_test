using Service.Security.Api.Core.Entities;

namespace Service.Security.Api.Core.Security
{
    public interface IJsonWebTokenGenerator
    {
        string GenerateToken(User user);
    }
}
