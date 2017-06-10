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
    public class AzureDBs1Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public AzureDBs1Controller(ApplicationDbContext context)
        {
            _context = context;    
        }


        // GET: AzureDBs1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Marketinfo_shop.ToListAsync());
        }

        // GET: AzureDBs1/Details/5
        public async Task<IActionResult> Details(string id)
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

        // GET: AzureDBs1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AzureDBs1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("縣市,市集名稱,市集介紹,營業時間,電話,GPS,營業地址,交通資訊")] AzureDB azureDB)
        {
            if (ModelState.IsValid)
            {
                _context.Add(azureDB);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(azureDB);
        }

        // GET: AzureDBs1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var azureDB = await _context.Marketinfo_shop.SingleOrDefaultAsync(m => m.縣市 == id);
            if (azureDB == null)
            {
                return NotFound();
            }
            return View(azureDB);
        }

        // POST: AzureDBs1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("縣市,市集名稱,市集介紹,營業時間,電話,GPS,營業地址,交通資訊")] AzureDB azureDB)
        {
            if (id != azureDB.縣市)
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
                    if (!AzureDBExists(azureDB.縣市))
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

        // GET: AzureDBs1/Delete/5
        public async Task<IActionResult> Delete(string id)
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

        // POST: AzureDBs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var azureDB = await _context.Marketinfo_shop.SingleOrDefaultAsync(m => m.縣市 == id);
            _context.Marketinfo_shop.Remove(azureDB);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AzureDBExists(string id)
        {
            return _context.Marketinfo_shop.Any(e => e.縣市 == id);
        }
    }
}
