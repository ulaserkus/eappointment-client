using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.DeletePatient;

public sealed record DeletePatientByIdCommand(Guid Id) : IRequest<Result<string>>;

