using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eAppointmentServer.Infrastructure.Services
{
    internal sealed class JwtProvider : IJwtProvider
    {
        public string CreateToken(AppUser user)
        {
            JwtSecurityToken jwtSecurityToken = new(
                issuer: "eAppointmentServer",
                audience: "eAppointment",
                claims: [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim("Username", user.UserName ?? string.Empty),
                  ],
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("YourSuperSecureLongKey_ChangeThisAndMakeItAtLeast64Characters_LONGER")),
                    SecurityAlgorithms.HmacSha512
                )
            );

            JwtSecurityTokenHandler tokenHandler = new();
            string token = tokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
