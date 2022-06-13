using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Security.Api.Core.Dto;
using Service.Security.Api.Core.Entities;
using Service.Security.Api.Core.Persistence;
using Service.Security.Api.Core.Security;

namespace Service.Security.Api.Core.Application
{
    public class Register
    {
        public class UserRegisterCommand : IRequest<UserDto>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UserRegisterValidator : AbstractValidator<UserRegisterCommand>
        {
            public UserRegisterValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, UserDto>
        {
            private readonly SecurityContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;
            private readonly IJsonWebTokenGenerator _jsonWebTokenGenerator;

            public UserRegisterHandler(SecurityContext context, UserManager<User> userManager, IMapper mapper, IJsonWebTokenGenerator jsonWebTokenGenerator)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
                _jsonWebTokenGenerator = jsonWebTokenGenerator;
            }

            public async Task<UserDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
            {
                var exists = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (exists)
                {
                    throw new Exception("The email is already registered");
                }

                exists = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if (exists)
                {
                    throw new Exception("The username is already registered");
                }

                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    UserName = request.UserName
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var dto = _mapper.Map<User, UserDto>(user);
                    dto.Token = _jsonWebTokenGenerator.GenerateToken(user);
                    return dto;
                }

                throw new Exception("Failed to register user");
            }
        }
    }
}
