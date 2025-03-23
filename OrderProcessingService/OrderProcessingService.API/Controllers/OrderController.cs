using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderProcessingService.Worker.Messages;

namespace OrderProcessingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderMessage order)
        {
            await _publishEndpoint.Publish(order);
            return Accepted(new { message = "Pedido enviado para a fila com sucesso." });
        }
    }
}
