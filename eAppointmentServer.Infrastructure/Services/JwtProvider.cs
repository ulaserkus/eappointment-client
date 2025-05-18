using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace eAppointmentServer.Infrastructure.Services
{
    internal sealed class JwtProvider(IConfiguration configuration,
        IUserRoleRepository userRoleRepository,
        RoleManager<AppRole> roleManager) : IJwtProvider
    {
        public async Task<string> CreateTokenAsync(AppUser user)
        {
            var appUserRoles = await userRoleRepository
                .Where(ur => ur.UserId == user.Id)
                .Join(
                    roleManager.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => new { ur, r }
                )
                .ToListAsync();

            List<AppRole> roles = appUserRoles.Select(ur => ur.r).ToList();
            List<string?> roleNames = roles.Select(r => r.Name).ToList();
            JwtSecurityToken jwtSecurityToken = new(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim("Username", user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Role, JsonSerializer.Serialize(roleNames)),
                  ],
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]!)),
                    SecurityAlgorithms.HmacSha512
                )
            );

            JwtSecurityTokenHandler tokenHandler = new();
            string token = tokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
