using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    public class ItemController : Controller
    {
        private readonly ItemContext _context;

        public ItemController(ItemContext context)
        {
            _context = context;
        }

        // GET: Item
        public IEnumerable<Item> GetItem()
        {
            return _context.DbItem.ToList();
        }

        [HttpGet]
        public IActionResult Add()
        {
               return PartialView("Add",new Item());
        }

        //POST: Item/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostItem([Bind("RenderingEngine,Browser,Platform,EngineVersion,CSSGrade")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item.ItemID == 0)
                    _context.Add(item);
                else
                    _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }


        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var item = await _context.DbItem.FindAsync(id);
            _context.DbItem.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
