using JustDo_Web.Interfaces;


namespace JustDo_Web.Models
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly string key;

        public JwtAuthManager(string key)
        {
            this.key = key;
        }

        public string Authenticate(string username, string password)
        {
            //todo : доделать создание Токена

            return null;
        }
    }
}
