using Microsoft.AspNetCore.Mvc;

namespace WebApiPatterns.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpGet("{orderId}")]
    public async Task<IActionResult> Get(int orderId)
    {
        await Task.Delay(100);

        return Ok($"Order {orderId}");
    }
}
