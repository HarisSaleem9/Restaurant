using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.User.Commands;
using Restaurants.Application.User.Commands.AssignUserRole;
using Restaurants.Application.User.Commands.Login;
using Restaurants.Application.User.Commands.Register;
using Restaurants.Application.User.Commands.UnassignUserRole;

namespace Restaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand updateUserDetailsCommand)
        {
            await _mediator.Send(updateUserDetailsCommand);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand)
        {
            var res = await _mediator.Send(registerUserCommand);
            return Ok(res);
        }
        [HttpPost("UserRole")]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }
        [HttpDelete("UnassignRole")]
        public async Task<IActionResult> UnAssignUserRole(UnassignUserRoleCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}
