using MediatR;
using OrderFlow.Application.DTOs;
using OrderFlow.Application.Features.Orders.Queries;
using OrderFlow.Domain.Interfaces.Repositories;

namespace OrderFlow.Application.Features.Orders.Handlers;

/// <summary>
/// Handler para processar a query que busca um pedido pelo ID.
/// </summary>
public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository; // Necessário para obter nomes de produtos

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);

        if (order == null)
        {
            return null;
        }

        // Mapeamento da entidade de domínio para DTO
        var orderDto = new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            TotalAmount = order.GetTotalAmount(),
            Items = []
        };

        foreach (var item in order.OrderItems)
        {
            // Para enriquecer o DTO, buscamos o nome do produto.
            // Em um cenário real, isso poderia ser otimizado com uma única consulta.
            var product = await _productRepository.GetByIdAsync(item.ProductId);

            orderDto.Items.Add(new OrderItemDto
            {
                ProductId = item.ProductId,
                ProductName = product?.Name ?? "Produto não encontrado",
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Total = item.GetTotal()
            });
        }

        return orderDto;
    }
}
