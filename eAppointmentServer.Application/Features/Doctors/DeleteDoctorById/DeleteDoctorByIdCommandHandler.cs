﻿using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.DeleteDoctorById;

internal sealed class DeleteDoctorByIdCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteDoctorByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDoctorByIdCommand request, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetByExpressionAsync(d => d.Id == request.Id, cancellationToken);
        if (doctor is null)
            return Result<string>.Failure("Doctor not found");

        doctorRepository.Delete(doctor);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return $"Doctor {doctor.Id} was deleted.";
    }
}

