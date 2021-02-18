
using JustDo_Web.Models;

namespace JustDo_Web.Services.Jwt
{
    public interface IJwtService
    {
        string CreateToken(User user);
    }
}
