using Microsoft.IdentityModel.Tokens;
using Service.Security.Api.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Security.Api.Core.Security
{
    public class JsonWebTokenGenerator : IJsonWebTokenGenerator
    {
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("username", user.UserName),
                new Claim("firstname", user.FirstName),
                new Claim("lastname", user.LastName),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("C7q3FBCJZq0bIRRH0Dq4lxWuBipEBkHX"));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
       
    }
}
