using MediatR;

namespace OrderFlow.Application.Features.Orders.Commands;

public record OrderItemInput(Guid ProductId, int Quantity);

public record CreateOrderCommand(Guid CustomerId, List<OrderItemInput> Items) : IRequest<Guid>;
