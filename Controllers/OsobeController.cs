using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPIntro.Data;
using ASPIntro.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASPIntro.Controllers
{
    public class OsobeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OsobeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Osobe
        public async Task<IActionResult> Index()
        {
            return View(await _context.Osoba.ToListAsync());
        }

        // GET: Osobe/Pretraga
        public IActionResult Pretraga()
        {
            return View();
        }

        public async Task<IActionResult> MTSBrojevi()
        {
            return View("Index", await _context.Osoba.Where(j => j.BrTelefona.StartsWith("064") || j.BrTelefona.StartsWith("066")).ToListAsync());
        }

        // POST: Osobe/PrikaziRezultatePretrage
        public async Task<IActionResult> PrikaziRezultatePretrage(String parametarPretrage)
        {
            return View("Index", await _context.Osoba.Where(j => j.Ime.Contains(parametarPretrage)).ToListAsync()) ;
        }

        // GET: Osobe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osoba = await _context.Osoba
                .FirstOrDefaultAsync(m => m.Id == id);
            if (osoba == null)
            {
                return NotFound();
            }

            return View(osoba);
        }

        // GET: Osobe/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Osobe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrTelefona,Ime,Id")] Osoba osoba)
        {
            string regEx1 = @"^(\+)(3816)([0-9]){6,9}$";

            if (ModelState.IsValid && System.Text.RegularExpressions.Regex.IsMatch(osoba.BrTelefona, regEx1))
            {
                _context.Add(osoba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
                return View("InputErrorView");
            return View(osoba);
        }

        // GET: Osobe/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osoba = await _context.Osoba.FindAsync(id);
            if (osoba == null)
            {
                return NotFound();
            }
            return View(osoba);
        }

        // POST: Osobe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrTelefona,Ime,Id")] Osoba osoba)
        {
            if (id != osoba.Id)
            {
                return NotFound();
            }

            string regEx1 = @"^(\+)(3816)([0-9]){6,9}$";

            if (ModelState.IsValid && System.Text.RegularExpressions.Regex.IsMatch(osoba.BrTelefona, regEx1))
            {
                try
                {
                    _context.Update(osoba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OsobaExists(osoba.Id))
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
            else
                return View("InputErrorView");
            return View(osoba);
        }

        // GET: Osobe/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osoba = await _context.Osoba
                .FirstOrDefaultAsync(m => m.Id == id);
            if (osoba == null)
            {
                return NotFound();
            }

            return View(osoba);
        }

        // POST: Osobe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var osoba = await _context.Osoba.FindAsync(id);
            _context.Osoba.Remove(osoba);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OsobaExists(int id)
        {
            return _context.Osoba.Any(e => e.Id == id);
        }
    }
}
