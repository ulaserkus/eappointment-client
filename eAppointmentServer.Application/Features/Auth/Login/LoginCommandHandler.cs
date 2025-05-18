using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Auth.Login;

internal sealed class LoginCommandHandler(UserManager<AppUser> userManager, IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{

    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.Users.FirstOrDefaultAsync(usr =>
        usr.Email == request.UserNameOrEmail ||
        usr.UserName == request.UserNameOrEmail);

        if (user is null)
        {
            return Result<LoginCommandResponse>.Failure("User not found");
        }

        var result = await userManager.CheckPasswordAsync(user, request.Password);
        if (!result)
        {
            return Result<LoginCommandResponse>.Failure("Invalid password");
        }

        string token = await jwtProvider.CreateTokenAsync(user);
        var response = new LoginCommandResponse(token);
        return Result<LoginCommandResponse>.Succeed(response);
    }
}