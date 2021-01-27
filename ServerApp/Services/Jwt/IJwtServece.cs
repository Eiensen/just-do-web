
using JustDo_Web.Models;

namespace JustDo_Web.Services.Jwt
{
    public interface IJwtServece
    {
        string CreateToken(User user);
    }
}
