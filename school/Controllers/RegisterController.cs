using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using school.Models;
using school.Data;
using school.ViewModels;
using school.DTOs;

public class RegisterController : Controller
{
    private readonly ApplicationDBContext _context;

    public RegisterController(ApplicationDBContext context)
    {
        _context = context;
    }

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

        var registrationsQuery = _context.Registers
            .Include(r => r.Student)
            .Include(r => r.Course)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(searchString))
        {
            registrationsQuery = registrationsQuery.Where(s => s.Student!.Name.ToUpper().Contains(searchString.ToUpper()));
        }

        if (!string.IsNullOrEmpty(selectedCourseName))
        {
            registrationsQuery = registrationsQuery.Where(x => x.Course!.CourseName == selectedCourseName);
        }

        // Projecting straight into DTO objects
        var dtos = await registrationsQuery.Select(r => new RegistrationDto
        {
            Id = r.Id,
            StudentId = r.StudentId,
            StudentName = r.Student != null ? r.Student.Name : "N/A",
            CourseId = r.CourseId,
            CourseName = r.Course != null ? r.Course.CourseName : "N/A",
            Grade = r.Grade
        }).ToListAsync();

        var registrationCourseVM = new RegisterCourseVM
        {
            Courses = new SelectList(await courseQuery.Distinct().ToListAsync()),
            Registrations = dtos,
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
            Id = id,
            StudentId = register.StudentId,
            Name = register.Student?.Name ?? "N/A",
            CourseName = register.Course?.CourseName ?? "N/A",
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
            // Map incoming safe DTO data directly to Database entity
            var register = new Register
            {
                StudentId = model.RegistrationData.StudentId,
                CourseId = model.RegistrationData.CourseId,
                Grade = model.RegistrationData.Grade
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

        // Package database parameters cleanly into the ViewModel using our Save DTO
        var viewModel = new RegistrationViewModel
        {
            RegistrationData = new SaveRegistrationDto
            {
                Id = register.Id,
                StudentId = register.StudentId,
                CourseId = register.CourseId,
                Grade = register.Grade
            },
            StudentOptions = await _context.Students.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToListAsync(),
            CourseOptions = await _context.Courses.Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.CourseName }).ToListAsync()
        };

        return View(viewModel);
    }

    // POST: REGISTERS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, RegistrationViewModel model)
    {
        if (id != model.RegistrationData.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Remap updated user DTO changes into a fresh tracking entity
                var register = new Register
                {
                    Id = model.RegistrationData.Id,
                    StudentId = model.RegistrationData.StudentId,
                    CourseId = model.RegistrationData.CourseId,
                    Grade = model.RegistrationData.Grade
                };

                _context.Update(register);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterExists(model.RegistrationData.Id))
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

        model.StudentOptions = await _context.Students.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToListAsync();
        model.CourseOptions = await _context.Courses.Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.CourseName }).ToListAsync();
        return View(model);
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
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (register == null)
        {
            return NotFound();
        }

        // Map to a clean, read-only DTO for safety
        var dto = new RegistrationDto
        {
            Id = register.Id,
            StudentName = register.Student?.Name ?? "N/A",
            CourseName = register.Course?.CourseName ?? "N/A",
            Grade = register.Grade
        };

        return View(dto);
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

    private bool RegisterExists(int id)
    {
        return _context.Registers.Any(e => e.Id == id);
    }
}
