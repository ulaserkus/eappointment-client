using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Roles.RoleSync;

internal sealed class RoleSyncCommandHandler(RoleManager<AppRole> roleManager) : IRequestHandler<RoleSyncCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RoleSyncCommand request, CancellationToken cancellationToken)
    {
        var currentRoles = await roleManager.Roles.ToListAsync(cancellationToken);
        var staticRoles = Constants.Roles;

        foreach (var role in currentRoles)
        {
            if(!staticRoles.Any(r=>r.Id == role.Id))
            {
               await roleManager.DeleteAsync(role);
            }
        }

        foreach (var role in staticRoles)
        {
            if (!currentRoles.Any(r => r.Id == role.Id))
            {
                await roleManager.CreateAsync(role);
            }
        }

        return Result<string>.Succeed("Roles synchronized successfully");
    }
}