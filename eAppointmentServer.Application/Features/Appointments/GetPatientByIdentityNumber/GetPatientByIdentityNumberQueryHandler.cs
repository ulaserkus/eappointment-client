using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetPatientByIdentityNumber;

internal class GetPatientByIdentityNumberQueryHandler(IPatientRepository patientRepository) : IRequestHandler<GetPatientByIdentityNumberQuery, Result<Patient>>
{
    public async Task<Result<Patient>> Handle(GetPatientByIdentityNumberQuery request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByExpressionAsync(p => p.IdentityNumber == request.identityNumber, cancellationToken);
        return patient;
    }
}