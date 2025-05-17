using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.DeletePatient;

internal sealed class DeletePatientByIdCommandHandler : IRequestHandler<DeletePatientByIdCommand, Result<string>>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeletePatientByIdCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<string>> Handle(DeletePatientByIdCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
        if (patient is null)
            return Result<string>.Failure("Patient not found");

        _patientRepository.Delete(patient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Patient deleted successfully");
    }
}

