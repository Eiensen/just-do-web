using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace JustDo_Web.Models
{
    public class AuthenticationOptions
    {
        public const string Issuer = "https://localhost:44344/"; 
        public const string Audience = "https://localhost:44344/"; 
        public const string Key = ".Application.ASP.Net_Core.v-5.0"; 
        public const int Lifetime = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
