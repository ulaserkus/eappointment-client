using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.DeleteAppointmentById;

internal sealed class DeleteAppointmentByIdCommandHandler : IRequestHandler<DeleteAppointmentByIdCommand, Result<string>>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteAppointmentByIdCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<string>> Handle(DeleteAppointmentByIdCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByExpressionAsync(a => a.Id == request.Id);
        
        if (appointment is null)
            return "Appointment not found";

        if(appointment.IsCompleted)
            return "Cannot delete a completed appointment";

        _appointmentRepository.Delete(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Appointment deleted successfully";
    }
}