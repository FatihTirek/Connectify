using System.Security.Cryptography;

namespace Connectify.src.Application.Helpers
{
    public class TokenHelper
    {
        public static string GenerateAuthToken()
        {
            var number = new byte[32];

            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(number);
            }

            return Convert.ToBase64String(number);
        }
    }
}