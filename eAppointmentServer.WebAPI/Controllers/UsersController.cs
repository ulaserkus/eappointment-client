using eAppointmentServer.Application.Features.Doctors.CreateDoctor;
using eAppointmentServer.Application.Features.Doctors.DeleteDoctorById;
using eAppointmentServer.Application.Features.Doctors.GetAllDoctors;
using eAppointmentServer.Application.Features.Doctors.UpdateDoctor;
using eAppointmentServer.Application.Features.Users.CreateUser;
using eAppointmentServer.Application.Features.Users.DeleteUserById;
using eAppointmentServer.Application.Features.Users.GetAllRolesForUsers;
using eAppointmentServer.Application.Features.Users.GetAllUsers;
using eAppointmentServer.Application.Features.Users.UpdateUser;
using eAppointmentServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.Controllers
{
  
    public class UsersController : ApiController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllRoles(GetAllRolesForUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserByIdCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
