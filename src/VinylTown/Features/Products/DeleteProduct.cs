using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;

namespace VinylTown.Features.Products;

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