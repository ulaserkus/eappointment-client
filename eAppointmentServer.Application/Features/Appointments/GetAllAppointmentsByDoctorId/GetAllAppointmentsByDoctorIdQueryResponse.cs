using eAppointmentServer.Domain.Entities;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointments;

public sealed record GetAllAppointmentsByDoctorIdQueryResponse(
    Guid Id,
    string Title,
    DateTime StartDate,
    DateTime EndDate,
    Patient Patient);
