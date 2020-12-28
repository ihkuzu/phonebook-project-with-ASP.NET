using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TelRehber.Data;
using TelRehber.Models;

namespace TelRehber.Controllers
{
    public class BilgilersController : Controller
    {
        private readonly RehberContext _context;

        public BilgilersController(RehberContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bilgiler.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilgiler = await _context.Bilgiler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bilgiler == null)
            {
                return NotFound();
            }

            return View(bilgiler);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AD,SOYAD,NUMARA")] Bilgiler bilgiler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bilgiler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bilgiler);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilgiler = await _context.Bilgiler.FindAsync(id);
            if (bilgiler == null)
            {
                return NotFound();
            }
            return View(bilgiler);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AD,SOYAD,NUMARA")] Bilgiler bilgiler)
        {
            if (id != bilgiler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bilgiler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BilgilerExists(bilgiler.Id))
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
            return View(bilgiler);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilgiler = await _context.Bilgiler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bilgiler == null)
            {
                return NotFound();
            }

            return View(bilgiler);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bilgiler = await _context.Bilgiler.FindAsync(id);
            _context.Bilgiler.Remove(bilgiler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BilgilerExists(int id)
        {
            return _context.Bilgiler.Any(e => e.Id == id);
        }
    }
}
