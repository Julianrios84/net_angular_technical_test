using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Categories.Api.Core.Persistence;

namespace Service.Categories.Api.Core.Application
{
    public class Delete
    {
        public class Run : IRequest
        {
            public int Id { get; set; }

        }

        public class Validation : AbstractValidator<Run>
        {
            public Validation()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Manager : IRequestHandler<Run>
        {
            private readonly CategoryContext _context;

            public Manager(CategoryContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Run request, CancellationToken cancellationToken)
            {
                var record = await _context.Categories.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (record == null)
                {
                    throw new Exception("Category not found");
                }

                // _context.Categories.Remove(record);
                record.SoftDelete = DateTime.Now;
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception("Category could not be deleting");
                }

                return Unit.Value;
            }
        }
    }
}
