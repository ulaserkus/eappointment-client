using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.UpdatePatient;

internal sealed class UpdatePatientByIdCommandHandler : IRequestHandler<UpdatePatientByIdCommand, Result<string>>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdatePatientByIdCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(UpdatePatientByIdCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
        if (patient is null)
            return Result<string>.Failure("Patient not found");
        var patientToUpdate = _mapper.Map<Patient>(request);
        _patientRepository.Update(patientToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return "Patient updated successfully";
    }
}
