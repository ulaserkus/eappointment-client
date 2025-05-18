using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper, IUserRoleRepository userRoleRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure("User not found");
        }

        if (user.Email != request.Email)
        {
            var emailExists = await userManager.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);
            if (emailExists)
                return Result<string>.Failure("Email already exists");
        }

        if (user.UserName != request.UserName)
        {
            var userNameExists = await userManager.Users.AnyAsync(u => u.UserName == request.UserName, cancellationToken);
            if (userNameExists)
                return Result<string>.Failure("Username already exists");
        }

        mapper.Map(request, user);
        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<string>.Failure(errors);
        }

        var existingUserRoles = await userRoleRepository.Where(ur => ur.UserId == user.Id).ToListAsync();
        userRoleRepository.DeleteRange(existingUserRoles);
        var userRoles = request.RoleIds.Select(roleId => new AppUserRole
        {
            UserId = user.Id,
            RoleId = roleId

        }).ToList();
        await userRoleRepository.AddRangeAsync(userRoles, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("User updated successfully");
    }
}
