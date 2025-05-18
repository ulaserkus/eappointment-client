using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.CreateUser;

internal sealed class CreateUserCommandHandler(UserManager<AppUser> userManager , IUserRoleRepository userRoleRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if(await userManager.Users.AnyAsync(p => p.Email == request.Email))
        {
            return Result<string>.Failure("User with this email already exists");
        }

        if (await userManager.Users.AnyAsync(p => p.UserName == request.UserName))
        {
            return Result<string>.Failure("User with this username already exists");
        }

        var user = mapper.Map<AppUser>(request);
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<string>.Failure(errors);
        }

        var userRoles = request.RoleIds.Select(roleId => new AppUserRole
        {
            UserId = user.Id,
            RoleId = roleId
        }).ToList();

        await userRoleRepository.AddRangeAsync(userRoles, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("User created successfully");
    }
}