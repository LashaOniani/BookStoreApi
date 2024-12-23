using BookStoreApi.Models;
using BookStoreApi.Packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IPKG_Orders order_package;

        public OrdersController(IPKG_Orders order_package)
        {
            this.order_package = order_package;
        }

        [HttpPost]
        public IActionResult AddOrder(OrderModel order)
        {
            try
            {
                order_package.add_oreder(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "შეცდომა მოხდა მოგვიანებით ცადეთ");
            }
        }

          [HttpGet]
          public IActionResult get_orders()
          {
              try
              {
                  List<OrderModel> orders = order_package.get_orders();
                  return Ok(orders);
              }
              catch (Exception ex)
              {
                  return StatusCode(StatusCodes.Status500InternalServerError, "შეცდომა მოხდა მოგვიანებით ცადეთ");
              }
          }
    }
}
