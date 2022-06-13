using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Service.Security.Api.Core.Dto;
using Service.Security.Api.Core.Entities;
using Service.Security.Api.Core.Persistence;
using Service.Security.Api.Core.Security;

namespace Service.Security.Api.Core.Application
{
    public class Login
    {
        public class UserLoginCommand : IRequest<UserDto>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UserLoginValidation : AbstractValidator<UserLoginCommand>
        {
            public UserLoginValidation()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class UserLoginHandler : IRequestHandler<UserLoginCommand, UserDto>
        {
            private readonly SecurityContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;
            private readonly IJsonWebTokenGenerator _jsonWebTokenGenerator;
            private readonly SignInManager<User> _signInManager;

            public UserLoginHandler(SecurityContext context, UserManager<User> userManager, IMapper mapper, IJsonWebTokenGenerator jsonWebTokenGenerator, SignInManager<User> signInManager)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
                _jsonWebTokenGenerator = jsonWebTokenGenerator;
                _signInManager = signInManager;
            }

            public async Task<UserDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new Exception("Username does not exist");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (result.Succeeded)
                {
                    var dto = _mapper.Map<User, UserDto>(user);
                    dto.Token = _jsonWebTokenGenerator.GenerateToken(user);
                    return dto;
                }

                throw new Exception("Wrong login");
            }
        }
    }
}
