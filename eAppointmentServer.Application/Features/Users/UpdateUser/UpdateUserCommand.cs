using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.UpdateUser;

public sealed record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    List<Guid> RoleIds) : IRequest<Result<string>>;
