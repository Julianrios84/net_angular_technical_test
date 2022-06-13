using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Categories.Api.Core.Application;
using Service.Categories.Api.Core.Dto;

namespace Service.Categories.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Run data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> Query()
        {
            return await _mediator.Send(new Query.Run());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> QueryById(int id)
        {
            return await _mediator.Send(new QueryById.Run { Id = id });
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(Update.Run data, int id)
        {
            data.Id = id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Run { Id = id });
        }
    }
}
