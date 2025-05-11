using eAppointmentServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.GetAllDoctors;

public sealed record GetAllDoctorsQuery() : IRequest<Result<List<Doctor>>>;

