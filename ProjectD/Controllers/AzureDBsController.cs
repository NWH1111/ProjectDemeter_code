using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectD.Data;
using ProjectD.Models;

namespace ProjectD.Controllers
{
    public class AzureDBsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AzureDBsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: AzureDBs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Marketinfo_shop.ToListAsync());
        }

        // GET: AzureDBs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var azureDB = await _context.Marketinfo_shop
                .SingleOrDefaultAsync(m => m.縣市 == id);
            if (azureDB == null)
            {
                return NotFound();
            }

            return View(azureDB);
        }

        // GET: AzureDBs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AzureDBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,CreationDate")] AzureDB azureDB)
        {
            if (ModelState.IsValid)
            {
                _context.Add(azureDB);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(azureDB);
        }

        // GET: AzureDBs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var azureDB = await _context.Marketinfo_shop.SingleOrDefaultAsync(m => m.Id == id);
            if (azureDB == null)
            {
                return NotFound();
            }
            return View(azureDB);
        }

        // POST: AzureDBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,CreationDate")] AzureDB azureDB)
        {
            if (id != azureDB.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(azureDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AzureDBExists(azureDB.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(azureDB);
        }

        // GET: AzureDBs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var azureDB = await _context.Marketinfo_shop
                .SingleOrDefaultAsync(m => m.Id == id);
            if (azureDB == null)
            {
                return NotFound();
            }

            return View(azureDB);
        }

        // POST: AzureDBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var azureDB = await _context.Marketinfo_shop.SingleOrDefaultAsync(m => m.Id == id);
            _context.Marketinfo_shop.Remove(azureDB);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AzureDBExists(int id)
        {
            return _context.Marketinfo_shop.Any(e => e.Id == id);
        }
    }
}
