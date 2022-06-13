using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Categories.Api.Core.Dto;
using Service.Categories.Api.Core.Entities;
using Service.Categories.Api.Core.Persistence;

namespace Service.Categories.Api.Core.Application
{
    public class Query
    {
        public class Run : IRequest<List<CategoryDto>> { }

        public class Manager : IRequestHandler<Run, List<CategoryDto>>
        {
            private readonly CategoryContext _context;
            private readonly IMapper _mapper;

            public Manager(CategoryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<CategoryDto>> Handle(Run request, CancellationToken cancellationToken)
            {
                //
                //var record = await _context.Categories.ToListAsync();
                var record = await _context.Categories.Where(x => x.SoftDelete == null).ToListAsync();
                var result = _mapper.Map<List<Category>, List<CategoryDto>>(record);
                return result;
            }
        }
    }
}
