using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;

namespace VinylTown.Features.Products;

[Route("api/Catalog")]
public class DeleteProductController : Controller
{
    private readonly IMediator _mediator;
	public DeleteProductController(IMediator mediator)
	{
		_mediator = mediator;
	}

    [HttpDelete("id")]
	public async Task<IActionResult> DeleteProductAsync(int id)
	{
        await _mediator.Send(new DeleteProductCommand(id));

        return NoContent();
	} 
}

public record DeleteProductCommand(int id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly AppDbContext _context;
    public DeleteProductCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.id, cancellationToken);

        if (product == null)
        {
            throw new Exception();
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}