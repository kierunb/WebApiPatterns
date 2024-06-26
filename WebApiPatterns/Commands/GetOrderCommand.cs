﻿using FluentValidation;
using MediatR;

namespace WebApiPatterns.Commands;

// 1) Define input data
// 2) Define output data
// 3) Define command handler (implementation)
// 4) Define validator for input data (optional)

// 1) class implementing IRequest<T> interface (DTO)
public record GetOrderRequest : IRequest<GetOrderResponse>
{
    public int OrderId { get; set; }
}

public class GetOrderRequestValidator : AbstractValidator<GetOrderRequest>
{
    public GetOrderRequestValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).Must(BeValidOrderId);
    }

    private bool BeValidOrderId(int orderId)
    {
        return orderId != 666;
    }
}

// 2) class with the result of the command (response) (DTO/ViewModel)
public record GetOrderResponse
{
    public int OrderId { get; set; }
    public string OrderName { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = string.Empty;
}

// 3) Command Handler -  class implementing IRequestHandler<TRequest, TResponse> interface

public class GetOrderCommandHandler : IRequestHandler<GetOrderRequest, GetOrderResponse>
{
    private readonly ILogger<GetOrderCommandHandler> _logger;

    // can be replaced by primary constructor syntax
    public GetOrderCommandHandler(ILogger<GetOrderCommandHandler> logger)
    {
        _logger = logger;
    }


    public async Task<GetOrderResponse> Handle(GetOrderRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(100);

        return new GetOrderResponse
        {
            OrderId = request.OrderId,
            OrderName = $"Order {request.OrderId}",
            OrderStatus = "Pending"
        };
    }
}
