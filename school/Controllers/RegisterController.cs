using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using school.Models;
using school.Data;
using school.ViewModels; // Added this to recognize your RegistrationViewModel

public class RegisterController : Controller
{
    private readonly ApplicationDBContext _context;

    public RegisterController(ApplicationDBContext context)
    {
        _context = context;
    }

    // GET: REGISTERS
    // GET: REGISTERS
    public async Task<IActionResult> Index(string selectedCourseName, string searchString)
    {
        if (_context.Registers == null)
        {
            return Problem("Entity set 'ApplicationDBContext.Registers' is null.");
        }
        IQueryable<string> courseQuery = from r in _context.Registers
                                         where r.Course != null
                                         orderby r.Course.CourseName
                                         select r.Course.CourseName;

        var registrations = from r in _context.Registers
                            .Include(r => r.Student)
                            .Include(r => r.Course)
                            select r;

      
        if (!string.IsNullOrEmpty(searchString))
        {
            registrations = registrations.Where(s => s.Student!.Name.ToUpper().Contains(searchString.ToUpper()));
        }

       
        if (!string.IsNullOrEmpty(selectedCourseName))
        {
            registrations = registrations.Where(x => x.Course!.CourseName == selectedCourseName);
        }

      
        var registrationCourseVM = new RegisterCourseVM
        {
            Courses = new SelectList(await courseQuery.Distinct().ToListAsync()),
            Registrations = await registrations.ToListAsync(),
            SearchString = searchString,
            SelectedCourseName = selectedCourseName
        };

        return View(registrationCourseVM);
    }


    // GET: REGISTERS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var register = await _context.Registers
            .Include(r => r.Student)
            .Include(r => r.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

      
        if (register == null)
        {
            return NotFound();
        }
        var detailVM = new RegisterDetailVM
        {
            Id=id,
            StudentId = register.StudentId,
            Name = register.Student.Name,
            CourseName=register.Course.CourseName,
            Grade = register.Grade,

        };

        return View(detailVM);
    }

    // GET: REGISTERS/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new RegistrationViewModel
        {
            StudentOptions = await _context.Students
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToListAsync(),

            CourseOptions = await _context.Courses
                .Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.CourseName })
                .ToListAsync()
        };

        return View(viewModel);
    }

    // POST: REGISTERS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    
    public async Task<IActionResult> Create(RegistrationViewModel model)
    {
        if (ModelState.IsValid)
        {
          
            var register = new Register
            {
                StudentId = model.StudentId,
                CourseId = model.CourseId,
                Grade = model.Grade
            };

            _context.Add(register);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

      
        model.StudentOptions = await _context.Students.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToListAsync();
        model.CourseOptions = await _context.Courses.Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.CourseName }).ToListAsync();
        return View(model);
    }

    // GET: REGISTERS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var register = await _context.Registers.FindAsync(id);
        if (register == null)
        {
            return NotFound();
        }
        return View(register);
    }

    // POST: REGISTERS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,StudentId,CourseId,Grade")] Register register)
    {
        if (id != register.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(register);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterExists(register.Id))
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
        return View(register);
    }

    // GET: REGISTERS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var register = await _context.Registers
            .Include(r => r.Student)
            .Include(r => r.Course)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (register == null)
        {
            return NotFound();
        }

        return View(register);
    }

    // POST: REGISTERS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var register = await _context.Registers.FindAsync(id);
        if (register != null)
        {
            _context.Registers.Remove(register);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RegisterExists(int? id)
    {
        return _context.Registers.Any(e => e.Id == id); 
    }
}
