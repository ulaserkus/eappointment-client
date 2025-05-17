using eAppointmentServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetPatientByIdentityNumber;

public record GetPatientByIdentityNumberQuery(string identityNumber) : IRequest<Result<Patient>>;
