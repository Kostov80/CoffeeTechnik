using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeeTechnik.Data;
using CoffeeTechnik.Models;

namespace CoffeeTechnik.Controllers
{
    public class MachinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MachinesController(ApplicationDbContext context)
        {

            _context = context;
        }

       

        public async Task<IActionResult> Index()
        {
            var machines = _context.Machines.Include(m => m.ObjectEntity);

            return View(await machines.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var machine = await _context.Machines.Include(m => m.ObjectEntity)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (machine == null) return NotFound();

            return View(machine);
        }

        
        public IActionResult Create()
        {
            ViewBag.ObjectEntityId = new SelectList(_context.Objects, "Id", "Address");

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,SerialNumber,InstallationDate,ObjectEntityId")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(machine);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ObjectEntityId = new SelectList(_context.Objects, "Id", "Address", machine.ObjectEntityId);

            return View(machine);
        }

        


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var machine = await _context.Machines.FindAsync(id);

            if (machine == null) return NotFound();

            ViewBag.ObjectEntityId = new SelectList(_context.Objects, "Id", "Address", machine.ObjectEntityId);

            return View(machine);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,SerialNumber,InstallationDate,ObjectEntityId")] Machine machine)
        {
            if (id != machine.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machine);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineExists(machine.Id))

                        return NotFound();
                    else

                        throw;
                }


                return RedirectToAction(nameof(Index));
            }

            ViewBag.ObjectEntityId = new SelectList(_context.Objects, "Id", "Address", machine.ObjectEntityId);
            return View(machine);
        }




        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var machine = await _context.Machines.Include(m => m.ObjectEntity)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (machine == null) return NotFound();

            return View(machine);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
           
            if (machine != null)
            {
                _context.Machines.Remove(machine);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        private bool MachineExists(int id)
        {
            return _context.Machines.Any(e => e.Id == id);
        }
    }
}
