
using JustDo_Web.Models;

namespace JustDo_Web.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}
