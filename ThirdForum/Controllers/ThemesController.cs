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
    public class ThemesController : Controller
    {
        private readonly DbContext_f _context;

        public ThemesController(DbContext_f context)
        {
            _context = context;
        }

        // GET: Themes
        [Route("admin/theme")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Theme.ToListAsync());
        }

        // GET: Themes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Theme == null)
            {
                return NotFound();
            }

            var theme = await _context.Theme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // GET: Themes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Themes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Theme theme)
        {
            var value = Request.Form["themename"];
            var x = new Theme
            {
                NomTheme = value
            };
            _context.Theme.Add(x);
            _context.SaveChanges();
            if (x!=null)
            {
                return RedirectToAction(nameof(Index));

            }

            return View(theme);
        }

        // GET: Themes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Theme == null)
            {
                return NotFound();
            }

            var theme = await _context.Theme.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }
            return View(theme);
        }

        // POST: Themes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomTheme")] Theme theme)
        {
            if (id != theme.Id)
            {
                return NotFound();
            }

    
             
                    _context.Theme.Update(theme);
                    _context.SaveChanges();
            if (theme!=null)
            {
                return RedirectToAction(nameof(Index));

            }

            return View(theme);
        }

        // GET: Themes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Theme == null)
            {
                return NotFound();
            }

            var theme = await _context.Theme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Theme == null)
            {
                return Problem("Entity set 'DbContext_f.Theme'  is null.");
            }
            var theme = await _context.Theme.FindAsync(id);
            if (theme != null)
            {
                _context.Theme.Remove(theme);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeExists(int id)
        {
          return _context.Theme.Any(e => e.Id == id);
        }
    }
}
