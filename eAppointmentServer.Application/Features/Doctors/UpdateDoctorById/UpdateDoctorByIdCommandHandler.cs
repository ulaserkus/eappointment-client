using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.UpdateDoctor;

internal sealed class UpdateDoctorByIdCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateDoctorByIdCommand, Result<string>>
{

    public async Task<Result<string>> Handle(UpdateDoctorByIdCommand request, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetByExpressionAsync(d => d.Id == request.Id, cancellationToken);

        if (doctor is null)
            return Result<string>.Failure("Doctor not found");

        var doctorToUpdate = mapper.Map<Doctor>(request);
        doctorRepository.Update(doctorToUpdate);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Doctor updated successfully");
    }
}