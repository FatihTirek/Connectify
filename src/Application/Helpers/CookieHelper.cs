namespace Connectify.src.Application.Helpers
{
    public class CookieHelper
    {
        private const string Domain = "localhost";
        
        public static void SetAuthToken(HttpResponse response, string authToken)
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Domain = Domain,
                IsEssential = true,
                Expires = DateTime.UtcNow.AddDays(7),
            };

            response.Cookies.Append("auth_token", authToken, options);
        }
    }
}