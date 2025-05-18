using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.DeleteUserById;

internal sealed class DeleteUserByIdCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<DeleteUserByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = userManager.Users.FirstOrDefault(u => u.Id == request.Id);
        if (user is null)
        {
            return Result<string>.Failure("User not found");
        }
        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("Failed to delete user");
        }
        return Result<string>.Succeed("User deleted successfully");
    }
}