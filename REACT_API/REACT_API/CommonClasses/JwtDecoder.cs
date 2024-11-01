namespace REACT_API.CommonClasses
{
    using System;
    using System.Text;
    using System.Text.Json;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;

    public class JwtDecoder
    {
        public static string DecodeJwt(string token, string secretKey)
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
            {
                throw new ArgumentException("JWT does not have 3 parts");
            }
            var header = parts[0];
            var payload = parts[1];

            string decodedHeader = Base64UrlDecode(header);
            string decodedPayload = Base64UrlDecode(payload);

            var signature = parts[2];
            var key = Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);

                Console.WriteLine("Token is valid.");
            }
            catch (SecurityTokenException ex)
            {
                Console.WriteLine("Token validation failed: " + ex.Message);
            }
            return decodedPayload;
        }

        private static string Base64UrlDecode(string input)
        {
            input = input.Replace('-', '+').Replace('_', '/');
            switch (input.Length % 4)
            {
                case 2: input += "=="; break;
                case 3: input += "="; break;
            }
            byte[] bytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(bytes);
        }
    }

}
