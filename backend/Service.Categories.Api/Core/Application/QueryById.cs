using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Categories.Api.Core.Dto;
using Service.Categories.Api.Core.Entities;
using Service.Categories.Api.Core.Persistence;

namespace Service.Categories.Api.Core.Application
{
    public class QueryById
    {
        public class Run : IRequest<CategoryDto>
        {
            public int Id { get; set; }
        }

        public class Manager : IRequestHandler<Run, CategoryDto>
        {
            private readonly CategoryContext _context;
            private readonly IMapper _mapper;

            public Manager(CategoryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CategoryDto> Handle(Run request, CancellationToken cancellationToken)
            {
                var record = await _context.Categories.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (record == null)
                {
                    throw new Exception("Category not found");
                }

                var result = _mapper.Map<Category, CategoryDto>(record);
                return result;
            }
        }
    }
}
