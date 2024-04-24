using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApiPatterns.Commands;

namespace WebApiPatterns.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // 1) Fluent validation integration with ASP.NET Core
    [HttpPost]
    
    public async Task<IActionResult> Post(GetOrderRequest orderRequest)
    {
        var response = await _mediator.Send(orderRequest);

        return Ok(response);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> Get(int orderId)
    {
        var response = await _mediator.Send(new GetOrderRequest { OrderId = orderId });

        return Ok(response);
    }
}
