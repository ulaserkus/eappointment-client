using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointments;

internal sealed class GetAllAppointmentsByDoctorIdQueryHandler : IRequestHandler<GetAllAppointmentsByDoctorIdQuery, Result<IEnumerable<GetAllAppointmentsByDoctorIdQueryResponse>>>
{
    private readonly IAppointmentRepository _appointmentRepository;
    public GetAllAppointmentsByDoctorIdQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
    public async Task<Result<IEnumerable<GetAllAppointmentsByDoctorIdQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdQuery request,
        CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository
                          .Where(a=>a.DoctorId == request.DoctorId)
                          .Include(a => a.Patient)
                          .ToListAsync(cancellationToken);

        var response = appointments.Select(a => new GetAllAppointmentsByDoctorIdQueryResponse(
                                    a.Id,
                                    a.Patient!.FullName,
                                    a.StartDate,
                                    a.EndDate,
                                    a.Patient))
                                    .ToList();

        return response;
    }
}
