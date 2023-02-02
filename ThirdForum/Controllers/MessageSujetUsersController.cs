using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThirdForum.Data;
using ThirdForum.Models;

namespace ThirdForum.Controllers
{
    public class MessageSujetUsersController : Controller
    {
        private readonly DbContext_f _context;

        public MessageSujetUsersController(DbContext_f context)
        {
            _context = context;
        }

        // GET: MessageSujetUsers
        [Route("admin/Allmesagges")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.MessageSujetUser.Include(x => x.User).Include(x => x.Sujets).Include(x => x.Message).ToListAsync());
        }

        // GET: MessageSujetUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MessageSujetUser == null)
            {
                return NotFound();
            }

            var messageSujetUser = await _context.MessageSujetUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageSujetUser == null)
            {
                return NotFound();
            }

            return View(messageSujetUser);
        }

        // GET: MessageSujetUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageSujetUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] MessageSujetUser messageSujetUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messageSujetUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messageSujetUser);
        }

        // GET: MessageSujetUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MessageSujetUser == null)
            {
                return NotFound();
            }

            var messageSujetUser = await _context.MessageSujetUser.FindAsync(id);
            if (messageSujetUser == null)
            {
                return NotFound();
            }
            return View(messageSujetUser);
        }

        // POST: MessageSujetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] MessageSujetUser messageSujetUser)
        {
            if (id != messageSujetUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageSujetUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageSujetUserExists(messageSujetUser.Id))
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
            return View(messageSujetUser);
        }

        // GET: MessageSujetUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MessageSujetUser == null)
            {
                return NotFound();
            }

            var messageSujetUser = await _context.MessageSujetUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageSujetUser == null)
            {
                return NotFound();
            }

            return View(messageSujetUser);
        }

        // POST: MessageSujetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MessageSujetUser == null)
            {
                return Problem("Entity set 'DbContext_f.MessageSujetUser'  is null.");
            }
            var messageSujetUser = await _context.MessageSujetUser.FindAsync(id);
            if (messageSujetUser != null)
            {
                _context.MessageSujetUser.Remove(messageSujetUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageSujetUserExists(int id)
        {
          return _context.MessageSujetUser.Any(e => e.Id == id);
        }
    }
}
