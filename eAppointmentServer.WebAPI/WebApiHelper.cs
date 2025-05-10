using eAppointmentServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace eAppointmentServer.WebAPI
{
    public static class WebApiHelper
    {
        public static async Task CreateUserIfNotExistsAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                if (!userManager.Users.Any())
                {
                    await userManager.CreateAsync(
                             new()
                             {
                                 FirstName = "Admin",
                                 LastName = "Admin",
                                 UserName = "admin",
                                 Email = "admin@admin.com",
                             }, "123aB***");
                }
            }
        }
    }
}
