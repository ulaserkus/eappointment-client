using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllDoctorsByDepartment;

internal sealed class GetAllDoctorsByDepartmentQueryHandler(IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorsByDepartmentQuery, Result<IEnumerable<Doctor>>>
{
    public async Task<Result<IEnumerable<Doctor>>> Handle(GetAllDoctorsByDepartmentQuery request,
        CancellationToken cancellationToken)
    {
        var doctors = await doctorRepository.Where(d => d.Department == request.DepartmentValue)
                                            .OrderBy(d => d.FirstName)
                                            .ToListAsync();
        return doctors;
    }
}

