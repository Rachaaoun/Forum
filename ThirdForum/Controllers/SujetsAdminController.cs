using System;
using System.Collections;
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
    public class SujetsAdminController : Controller
    {
        private readonly DbContext_f _context;

        public SujetsAdminController(DbContext_f context)
        {
            _context = context;
        }

        // GET: SujetsAdmin
        [Route("admin/sujets")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Sujets.Include(x => x.Theme).ToListAsync());
        }

        // GET: SujetsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sujets == null)
            {
                return NotFound();
            }

            var sujet = await _context.Sujets.Include(w => w.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sujet == null)
            {
                return NotFound();
            }

            return View(sujet);
        }

        // GET: SujetsAdmin/Create
        public IActionResult Create()
        {
            
            var listThemes =  _context.Theme.ToList();
            ViewBag.MyList = listThemes;
            return View();
        }

        // POST: SujetsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sujet sujet)
        {
            //[Bind("Id,NomSujet,Photo,Description,1")] 
            var listThemes = _context.Theme.ToList();
            ViewBag.MyList = listThemes;
            var nom = Request.Form["NomSujet"];
                var photo = Request.Form["Photo"];
                var description = Request.Form["Description"];
            var themeId = Request.Form["Theme"];
            var theme = await _context.Theme.FindAsync(int.Parse(themeId));
                var x = new Sujet
                {
                    NomSujet = nom,
                    Photo= photo,
                    Description= description,
                    Theme = theme
                };
                _context.Add(x);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: SujetsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var listThemes = _context.Theme.ToList();
            ViewBag.MyList = listThemes;
            if (id == null || _context.Sujets == null)
            {
                return NotFound();
            }

            var sujet = await _context.Sujets.FindAsync(id);
            if (sujet == null)
            {
                return NotFound();
            }

            return View(sujet);
        }


        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Theme
                                   orderby d.NomTheme
                                   select d;
            ViewBag.theme_id = new SelectList(departmentsQuery, "theme_id", "NomTheme", selectedDepartment);
        }

        // POST: SujetsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomSujet,Photo,Description,Theme")] Sujet sujet)
        {
            if (id != sujet.Id)
            {
                return NotFound();
            }
            var listThemes = _context.Theme.ToList();
            ViewBag.MyList = listThemes;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sujet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SujetExists(sujet.Id))
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
            return View(sujet);
        }

        // GET: SujetsAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sujets == null)
            {
                return NotFound();
            }

            var sujet = await _context.Sujets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sujet == null)
            {
                return NotFound();
            }

            return View(sujet);
        }

        // POST: SujetsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sujets == null)
            {
                return Problem("Entity set 'DbContext_f.Sujets'  is null.");
            }
            var sujet = await _context.Sujets.FindAsync(id);
            if (sujet != null)
            {
                _context.Sujets.Remove(sujet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SujetExists(int id)
        {
          return _context.Sujets.Any(e => e.Id == id);
        }
    }
}
