using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Categories.Api.Core.Entities;
using Service.Categories.Api.Core.Persistence;

namespace Service.Categories.Api.Core.Application
{
    public class Create
    {
        public class Run : IRequest
        {
            public string Code { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int? IdParentCategory { get; set; }

        }

        public class Validation : AbstractValidator<Run>
        {
            public Validation()
            {
                RuleFor(x => x.Code).NotEmpty().Length(2, 10);
                RuleFor(x => x.Title).NotEmpty().Length(2, 10);
                RuleFor(x => x.Description).NotEmpty().Length(10, 500);
                //RuleFor(x => x.softDelete).Must(BeAValidDate).WithMessage("Invalid date/time");
            }

            /*private bool BeAValidDate(DateTime? date)
            {
                if (date == default(DateTime))
                    return false;
                return true;
            }*/
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

                if (request.IdParentCategory != null || request.IdParentCategory.HasValue)
                {
                    var exist = await _context.Categories.Where(x => x.Id == request.IdParentCategory).FirstOrDefaultAsync();
                    if (exist == null)
                    {
                        throw new Exception("Parent category does not exist");
                    }

                    var count = await _context.Categories.Where(x => x.IdParentCategory == request.IdParentCategory).Where(x => x.SoftDelete == null).CountAsync();
                    if(count >= 3)
                    {
                        throw new Exception("This category has already reached its subcategory limit");
                    }
                }

                var category = new Category
                {
                    Code = request.Code,
                    Title = request.Title,
                    Description = request.Description,
                    IdParentCategory = (int) request.IdParentCategory,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                await _context.Categories.AddAsync(category);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception("Category could not be registered");
                }

                return Unit.Value;
            }
        }
    }
}
