using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Security.Api.Core.Application;
using Service.Security.Api.Core.Dto;

namespace Service.Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserDto>> SignUp(Register.UserRegisterCommand parameters)
        {
            return await _mediator.Send(parameters);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(Login.UserLoginCommand parameters)
        {
            return await _mediator.Send(parameters);
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> Get()
        {
            return await _mediator.Send(new UserCurrent.UserCurrentCommand());
        }
    }
}
