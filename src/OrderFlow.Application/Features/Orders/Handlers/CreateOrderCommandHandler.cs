using MediatR;
using OrderFlow.Application.Features.Orders.Commands;
using OrderFlow.Domain.Entities;
using OrderFlow.Domain.Interfaces;
using OrderFlow.Domain.Interfaces.Repositories;

namespace OrderFlow.Application.Features.Orders.Handlers;

/// <summary>
/// Handler para processar o comando de criação de pedido.
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // 1. Validação
        if (request.Items == null || request.Items.Count == 0)
        {
            throw new ArgumentException("O pedido deve conter pelo menos um item.");
        }

        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Cliente com ID '{request.CustomerId}' não encontrado.");
        }

        // 2. Criação da Entidade de Domínio
        var order = new Order(Guid.NewGuid(), request.CustomerId);

        foreach (var itemInput in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemInput.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Produto com ID '{itemInput.ProductId}' não encontrado.");
            }

            // 3. Utilização do método de domínio para adicionar o item
            // Isso garante que as regras de negócio da entidade (ex: snapshot de preço) sejam aplicadas.
            order.AddItem(product, itemInput.Quantity);
        }

        // 4. Persistência
        await _orderRepository.AddAsync(order);

        // 5. Retorno do resultado
        return order.Id;
    }
}
