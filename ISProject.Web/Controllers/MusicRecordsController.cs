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

namespace ISProject.Web.Controllers
{
    public class MusicRecordsController : Controller
    {
        
        private readonly IMusicRecordService _musicRecordService;

        public MusicRecordsController(IMusicRecordService musicRecordService)
        {
            _musicRecordService = musicRecordService;
        }
        

        // GET: MusicRecords
        public async Task<IActionResult> Index()
        {
            return View(_musicRecordService.GetAll());
        }

        // GET: MusicRecords/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicRecord = _musicRecordService.GetRecordById(id);
            if (musicRecord == null)
            {
                return NotFound();
            }

            return View(musicRecord);
        }

        // GET: MusicRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MusicRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Artist,Price,Volume,InStock,ImageURL,Id")] MusicRecord musicRecord)
        {
            if (ModelState.IsValid)
            {
                _musicRecordService.CreateRecord(musicRecord);
                return RedirectToAction(nameof(Index));
            }
            return View(musicRecord);
        }

        // GET: MusicRecords/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicRecord = _musicRecordService.GetRecordById(id) as MusicRecord;
            if (musicRecord == null)
            {
                return NotFound();
            }
            return View(musicRecord);
        }

        // POST: MusicRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _musicRecordService.UpdateRecord(musicRecord);
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

        // GET: MusicRecords/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicRecord = _musicRecordService.GetRecordById(id);
            if (musicRecord == null)
            {
                return NotFound();
            }

            return View(musicRecord);
        }

        // POST: MusicRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            var musicRecord = _musicRecordService.GetRecordById(id);
            if (musicRecord != null)
            {
                _musicRecordService.DeleteRecord(musicRecord.Id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MusicRecordExists(Guid id)
        {
            
            return _musicRecordService.GetAll().Any(e => e.Id == id);
        }
    }
}
