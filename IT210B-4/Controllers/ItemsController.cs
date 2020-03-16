using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using IT210B_4.Data;
using IT210B_4.Data.Dao;
using IT210B_4.Models;
using System.Security.Claims;

namespace IT210B_4.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IItemDao _dao;

        public ItemsController(IAtlasSettings settings)
        {
            _dao = new ItemDao(settings);
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Item> items = await _dao.Read(UserId);
            ViewData["list"] = items;
            ViewData["userId"] = UserId;
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Text,Date")] Item item)
        {
            if (ModelState.IsValid)
            {
                await _dao.Create(item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,Text,Done,Date")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    item.Done = !item.Done;
                    await _dao.Update(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _dao.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
