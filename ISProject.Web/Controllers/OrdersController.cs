using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISProject.Domain;
using ISProject.Repository;
using ISProject.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using ISProject.Service.Implementation;
using Microsoft.AspNetCore.Identity;
using ISProject.Domain.Identity;

namespace ISProject.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<MusicStoreUser> _userManager;

        public OrdersController(IOrderService orderService, UserManager<MusicStoreUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrders();
            return View(orders);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderDetails(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderDetails(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> CreateOrder(Guid cartId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var order = await _orderService.CreateOrder(user, cartId);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid Id)
        {
            if (Id != null)
            {
                await _orderService.DeleteOrder(Id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            var orders = _orderService.GetOrders().GetAwaiter().GetResult();
            return orders.Any(o => o.Id == id);
        }
    }
}
