using MediatR;

namespace VinylTown.Features.Cart;

public record AddItemCommand : IRequest;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand>
