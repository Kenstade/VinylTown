﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using VinylTown.Data;
using VinylTown.Domain;

namespace VinylTown.Features.Products;

public record CreateProductCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; } = string.Empty;
    public int Stock { get; init; }
    public int ProductAuthorId { get; init; }
    public int ProductGenreId { get; init; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly AppDbContext _context;
    public CreateProductCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product();

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.Image = request.Image;
        entity.Stock = request.Stock;
        entity.ProductAuthorId = request.ProductAuthorId;
        entity.ProductGenreId = request.ProductGenreId;

        _context.Products.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}