using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Categories.Api.Core.Persistence;

namespace Service.Categories.Api.Core.Application
{
    public class Update
    {
        public class Run : IRequest<int>
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int? IdParentCategory { get; set; }

        }

        public class Validation : AbstractValidator<Run>
        {
            public Validation()
            {
                RuleFor(x => x.Code).NotEmpty();
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
            }
        }

        public class Manager : IRequestHandler<Run, int>
        {
            private readonly CategoryContext _context;

            public Manager(CategoryContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(Run request, CancellationToken cancellationToken)
            {
                if(request.Id == request.IdParentCategory)
                {
                    throw new Exception("Category cannot be its own parent");
                }

                var record = await _context.Categories.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (record == null)
                {
                    throw new Exception("Category not found");
                }

                if (request.IdParentCategory != null || request.IdParentCategory.HasValue)
                {
                    var exist = await _context.Categories.Where(x => x.Id == request.IdParentCategory).FirstOrDefaultAsync();
                    if (exist == null)
                    {
                        throw new Exception("Parent category does not exist");
                    }

                    var count = await _context.Categories.Where(x => x.IdParentCategory == request.IdParentCategory).Where(x => x.SoftDelete == null).CountAsync();
                    if (count >= 3)
                    {
                        throw new Exception("This category has already reached its subcategory limit");
                    }
                }

                record.Code = request.Code;
                record.Title = request.Title;
                record.Description = request.Description;
                record.IdParentCategory = request.IdParentCategory;
                record.UpdateDate = DateTime.Now;

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception("Category could not be updated");
                }

                return record.Id;
            }
        }
    }
}
