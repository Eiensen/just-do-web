
namespace JustDo_Web.Interfaces
{
    public interface IJwtAuthManager
    {
        string Authenticate(string username, string password);
    }
}
