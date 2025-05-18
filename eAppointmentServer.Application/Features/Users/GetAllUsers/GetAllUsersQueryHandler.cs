using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(UserManager<AppUser> userManager,
    IUserRoleRepository userRoleRepository) : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<GetAllUsersQueryResponse>>>
{
    public async Task<Result<IEnumerable<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users
            .OrderBy(u => u.FirstName)
            .ToListAsync(cancellationToken);
      
        var response = users.Select(u => new GetAllUsersQueryResponse(
                                             u.Id,
                                             u.UserName ?? string.Empty, // Handle possible null
                                             u.Email ?? string.Empty,    // Handle possible null
                                             u.FirstName,
                                             u.LastName,
                                             u.FullName,
                                             userRoleRepository.Where(ur => ur.UserId == u.Id)
                                                 .Select(ur => ur.RoleId)
                                                 .ToList())
                                           )
                                           .ToList();
        return response;
    }
}