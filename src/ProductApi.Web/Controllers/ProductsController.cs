using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs.Requests;
using ProductApi.Application.Features.Products.Commands;
using ProductApi.Application.Features.Products.Queries;

namespace ProductApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductDto dto)
        {
            var command = new CreateProductCommand(dto);
            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProductDto dto)
        {
            var command = new UpdateProductCommand(id, dto);
            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return NoContent();
        }
    }
}
