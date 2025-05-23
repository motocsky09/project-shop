using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Entities;
using Server.Repositories;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IConfiguration configuration, IOrderRepository orderRepository)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [Route("GetOrderById")]
        public ActionResult GetOrderById(int orderid)
        {
            var result = _orderRepository.GetOrderById(orderid);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetOrder")]
        public ActionResult GetOrders()
        {
            var result = _orderRepository.GetOrders();
            return Ok(result);
        }

        [HttpPost]
[Route("CreateOrder")]
public ActionResult CreateOrder(
    [FromQuery] string userId,
    [FromQuery] string shoppingCartId,
    [FromQuery] int sumDelivery,
    [FromQuery] int totalSumWithDelivery,
    [FromQuery] string Address,
    [FromQuery] string City,
    [FromQuery] string PhoneNumber,
    [FromQuery] string Email,
    [FromQuery] string Postal,
    [FromQuery] string Comments = "") // Valoare implicită
{
    _orderRepository.CreateOrder(userId, shoppingCartId, sumDelivery, totalSumWithDelivery, Address,City, PhoneNumber, Email, Postal, Comments ?? "");
    return Ok();
}

        [HttpPut]
        [Route("UpdateOrder")]
        public ActionResult UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
            return Ok(order);
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public ActionResult DeleteOrder(int orderid)
        {
            _orderRepository.DeleteOrder(orderid);
            return Ok();
        }
    }
}
