using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeeTechnik.Data;
using CoffeeTechnik.Models;

namespace CoffeeTechnik.Controllers
{
    public class MachinesController : Controller//управлява CRUD операциите за машините
    {
        private readonly ApplicationDbContext _context;

        public MachinesController(ApplicationDbContext context)
        {

            _context = context;
        }

       

        public async Task<IActionResult> Index()// машините 
        {
            var machines = _context.Machines.Include(m => m.ObjectEntity);

            return View(await machines.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)//показва детайлите на конкретна машина
        {
            if (id == null) return NotFound();

            var machine = await _context.Machines.Include(m => m.ObjectEntity)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (machine == null) return NotFound();

            return View(machine);
        }

        
        public IActionResult Create()//формата създаване на нова машина
        {
            ViewBag.ObjectEntityId = new SelectList(_context.Objects, "Id", "Address");

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]//обработва данните за създаване на нова машина
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

        


        public async Task<IActionResult> Edit(int? id)//формата за редактиране на съществуваща машина
        {
            if (id == null) return NotFound();

            var machine = await _context.Machines.FindAsync(id);

            if (machine == null) return NotFound();

            ViewBag.ObjectEntityId = new SelectList(_context.Objects, "Id", "Address", machine.ObjectEntityId);

            return View(machine);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]  //обработва данните за редактиране на съществуваща машина
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




        
        public async Task<IActionResult> Delete(int? id)//формата за потвърждение на изтриване на машина
        {
            if (id == null) return NotFound();

            var machine = await _context.Machines.Include(m => m.ObjectEntity)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (machine == null) return NotFound();

            return View(machine);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)//обработва изтриването на машина след потвърждение
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
