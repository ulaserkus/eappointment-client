using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.CreateAppointment;

internal sealed class CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork, IPatientRepository patientRepository) : IRequestHandler<CreateAppointmentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        Guid patientId = request.PatientId ?? Guid.Empty;

        if (request.StartDate == null || request.EndDate == null)
            return Result<Guid>.Failure("Start date and end date cannot be null");

        if (request.DoctorId == Guid.Empty)
            return Result<Guid>.Failure("Doctor ID cannot be empty");

 
        var startDate = Convert.ToDateTime(request.StartDate);
        var endDate = Convert.ToDateTime(request.EndDate);

        if (startDate >= endDate)
            return Result<Guid>.Failure("Start date must be earlier than end date");

        if (patientId == Guid.Empty)
        {
            var patient = new Patient
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityNumber = request.IdentityNumber,
                City = request.City,
                Town = request.Town,
                FullAddress = request.FullAddress
            };

            var existingPatient = await patientRepository.GetByExpressionAsync(p => p.IdentityNumber == request.IdentityNumber, cancellationToken);

            if (existingPatient is not null)
                return Result<Guid>.Failure("Patient with this identity number already exists");

            await patientRepository.AddAsync(patient, cancellationToken);
            patientId = patient.Id;

        }

        var appointment = new Appointment
        {
            StartDate = startDate,
            EndDate = endDate,
            DoctorId = request.DoctorId,
            PatientId = patientId
        };

        await appointmentRepository.AddAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return appointment.Id;
    }
}
