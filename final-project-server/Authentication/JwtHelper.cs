using final_project_server.Models.Users.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace final_project_server.Authentication
{

    public class JwtHelper
    {
        private static string _jwtKey;

        public JwtHelper(IOptions<JwtPOCO> jwtConfig)
        {
            _jwtKey = jwtConfig.Value.Key;
        }

        public static string GenerateAuthToken(UserSQL user)
        {
            if (string.IsNullOrEmpty(_jwtKey))
            {
                throw new InvalidOperationException("JWT key is not initialized.");
            }
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenClaims = new Claim[]
            {
                new Claim("id",user.Id.ToString()),
                new Claim("firstName",user.FirstName.ToString()),
                new Claim("isAdmin",user.IsAdmin.ToString()),
                new Claim("profilePicture",user.ProfilePicture)
            };

            var token = new JwtSecurityToken(
                issuer: "PetitionBackEnd",
                audience: "PetitionFrontEnd",
                claims: tokenClaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
