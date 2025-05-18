using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.GetAllRolesForUsers;

internal sealed class GetAllRolesForUsersQueryHandler(RoleManager<AppRole> roleManager) : IRequestHandler<GetAllRolesForUsersQuery, Result<IEnumerable<AppRole>>>
{
    public async Task<Result<IEnumerable<AppRole>>> Handle(GetAllRolesForUsersQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
        return roles;
    }
}
