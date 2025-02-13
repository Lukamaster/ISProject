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
using System.Runtime.CompilerServices;
using ISProject.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace ISProject.Web.Controllers
{
    [Authorize]
    public class MusicRecordsController : Controller
    {
        private readonly UserManager<MusicStoreUser> _userManager;
        private readonly IMusicRecordService _musicRecordService;
        private readonly IShoppingCartService _shoppingCartService;

        public MusicRecordsController(IMusicRecordService musicRecordService, UserManager<MusicStoreUser> userManager, IShoppingCartService shoppingCartService)
        {
            _musicRecordService = musicRecordService;
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var musicRecords = await _musicRecordService.GetAll();
            return View(musicRecords);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicRecord = await _musicRecordService.GetRecordById(id);
            if (musicRecord == null)
            {
                return NotFound();
            }

            return View(musicRecord);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Artist,Price,Volume,InStock,ImageURL,Id")] MusicRecord musicRecord)
        {
            if (ModelState.IsValid)
            {
                await _musicRecordService.CreateRecord(musicRecord);
                return RedirectToAction(nameof(Index));
            }
            return View(musicRecord);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicRecord = await _musicRecordService.GetRecordById(id);
            if (musicRecord == null)
            {
                return NotFound();
            }
            return View(musicRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,Artist,Price,Volume,InStock,ImageURL,Id")] MusicRecord musicRecord)
        {
            if (id != musicRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _musicRecordService.UpdateRecord(musicRecord);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicRecordExists(musicRecord.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(musicRecord);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicRecord = await _musicRecordService.GetRecordById(id);
            if (musicRecord == null)
            {
                return NotFound();
            }

            return View(musicRecord);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            var musicRecord = await _musicRecordService.GetRecordById(id);
            if (musicRecord != null)
            {
                await _musicRecordService.DeleteRecordAsync(musicRecord.Id);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddToCart(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var musicRecord = await _musicRecordService.GetRecordById(id);

            if (musicRecord != null && user != null)
            {
                await _shoppingCartService.AddProductToShoppingCart(user.Id, musicRecord);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _shoppingCartService.DeleteFromShoppingCart(user.Id, id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MusicRecordExists(Guid id)
        {
            var musicRecords = _musicRecordService.GetAll().GetAwaiter().GetResult();
            return musicRecords.Any(r => r.Id == id);
        }
    }
}
