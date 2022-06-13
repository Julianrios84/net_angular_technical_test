using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Service.Security.Api.Core.Dto;
using Service.Security.Api.Core.Entities;
using Service.Security.Api.Core.Security;

namespace Service.Security.Api.Core.Application
{
    public class UserCurrent
    {
        public class UserCurrentCommand : IRequest<UserDto> { }

        public class UserCurrentHandler : IRequestHandler<UserCurrentCommand, UserDto>
        {
            private readonly UserManager<User> _userManager;
            private readonly IUserInSession _userInSession;
            private readonly IJsonWebTokenGenerator _jsonWebTokenGenerator;
            private readonly IMapper _mapper;

            public UserCurrentHandler(UserManager<User> userManager, IUserInSession userInSession, IJsonWebTokenGenerator jsonWebTokenGenerator, IMapper mapper)
            {
                _userManager = userManager;
                _userInSession = userInSession;
                _jsonWebTokenGenerator = jsonWebTokenGenerator;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(UserCurrentCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userInSession.GetUserInSession());
                if (user != null)
                {
                    var dto = _mapper.Map<User, UserDto>(user);
                    dto.Token = _jsonWebTokenGenerator.GenerateToken(user);
                    return dto;
                }

                throw new Exception("User not found");
            }
        }
    }
}
