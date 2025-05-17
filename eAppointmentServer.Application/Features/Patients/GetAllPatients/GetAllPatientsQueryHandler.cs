using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.GetAllPatients;

internal sealed class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, Result<List<Patient>>>
{
    private readonly IPatientRepository _patientRepository;
    public GetAllPatientsQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    public async Task<Result<List<Patient>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        var patients = await _patientRepository
        .GetAll()
        .OrderBy(p => p.LastName)
        .ThenBy(p => p.FirstName)
        .ToListAsync(cancellationToken);

        return patients;
    }
}