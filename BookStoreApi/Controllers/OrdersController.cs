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

        [HttpGet]
        public IActionResult Get_User_s_Orders()
        {
            try
            {
                var orders = order_package.get_users_orders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "მომხმარებლის შეკვეთები ვერ მოიძებნა");
            }
        }

        [HttpGet]
        public IActionResult Get_User_Orders(string customer)
        {
            try
            {
                var orders = order_package.get_user_orders(customer);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "მომხმარებლის შეკვეთები ვერ მოიძებნა");
            }
        }

        [HttpPost]
        public IActionResult Get_user_each_order(OrderModel order)
        {
            try
            {
                var orders = order_package.get_user_each_orders(order);
                return Ok(orders);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "მომხმარებლის შეკვეთები ვერ მოიძებნა");
            }
        }

        [HttpPost]
        public IActionResult Update_Order_status(OrderModel order)
        {
            try
            {
                order_package.update_order_status(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "ცლვილება ვერ მოხერხდა მოგვიანებით ცადეთ");
            }
        }
    }
}
