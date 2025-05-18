namespace eAppointmentServer.Application.Features.Users.GetAllUsers;

public sealed record GetAllUsersQueryResponse(
    Guid Id,
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string FullName,
    List<Guid> RoleIds
    );
