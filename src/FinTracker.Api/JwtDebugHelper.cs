using System.IdentityModel.Tokens.Jwt;

namespace FinTracker.Api
{
    public static class JwtDebugHelper
    {
        public static void DecodeToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    Console.WriteLine("Token Claims:");
                    foreach (var claim in jsonToken.Claims)
                    {
                        Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error decoding token: {ex.Message}");
            }
        }
    }
}
