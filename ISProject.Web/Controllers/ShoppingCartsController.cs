using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISProject.Domain;
using ISProject.Repository;
using ISProject.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using ISProject.Domain.Identity;
using ISProject.Service.Interface;

namespace ISProject.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly UserManager<MusicStoreUser> _userManager;
        private readonly ApplicationDbContext _context;
        public ShoppingCartsController(IShoppingCartService shoppingCartService, UserManager<MusicStoreUser> userManager, ApplicationDbContext context)
        {
            _shoppingCartService = shoppingCartService;
            _userManager = userManager;
            _context = context;
        }

        // GET: ShoppingCarts
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.ShoppingCarts.Include(s => s.Owner);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: ShoppingCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var shoppingCart = await _shoppingCartService.GetShoppingCartDetails(user.Id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCarts.Remove(shoppingCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(Guid id)
        {
            return _context.ShoppingCarts.Any(e => e.Id == id);
        }
    }
}
