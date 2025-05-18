using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    List<Guid> RoleIds) : IRequest<Result<string>>;
