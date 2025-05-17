using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.UpdateAppointment;

internal sealed class UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateAppointmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.GetByExpressionWithTrackingAsync(a => a.Id == request.Id, cancellationToken);

        if (appointment is null)
            return Result<string>.Failure("Appointment not found");

        var startDate = Convert.ToDateTime(request.StartDate);
        var endDate = Convert.ToDateTime(request.EndDate);

        var isAppointmentDateValid = await appointmentRepository.AnyAsync(a =>
         (a.EndDate > startDate && a.StartDate <= endDate) ||
         (a.StartDate >= startDate && a.EndDate <= endDate) ||
            (a.StartDate <= startDate && a.EndDate >= endDate)
        );

        if (isAppointmentDateValid)
            return Result<string>.Failure("Appointment date is not valid");

        appointment.StartDate = startDate;
        appointment.EndDate = endDate;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Appointment updated successfully");
    }
}