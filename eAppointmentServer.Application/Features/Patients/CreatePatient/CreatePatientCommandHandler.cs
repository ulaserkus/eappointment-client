using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.CreatePatient;

internal sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<Guid>>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePatientCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<Guid>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var existingPatient = await _patientRepository.GetByExpressionAsync(p => p.IdentityNumber == request.IdentityNumber, cancellationToken);
        if (existingPatient is not null)
            return Result<Guid>.Failure("Patient with this identity number already exists");

        var patient = _mapper.Map<Patient>(request);
        await _patientRepository.AddAsync(patient, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return patient.Id;
    }
}
