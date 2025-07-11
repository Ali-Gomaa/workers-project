using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Data;
using Workers.Models;

namespace Workers.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.People)
                .ToListAsync();
            ViewBag.JobTitles = await _context.JobTitles.ToListAsync(); // 👈 دي السطر المهم

            return View(projects);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MovePerson(int personId, int targetProjectId)
        {
            var person = await _context.People.FindAsync(personId);
            if (person == null)
                return NotFound();

            person.ProjectId = targetProjectId;
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return RedirectToAction("Index");

            var project = new Project { Name = name };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPerson(string name, int jobTitleId, int projectId)
        {
            if (string.IsNullOrWhiteSpace(name))
                return RedirectToAction("Index");

            // تحقق لو المسمى الوظيفي "مدير" (Id == مثلا 2) وتأكد مفيش مدير تاني
            var jobTitle = await _context.JobTitles.FindAsync(jobTitleId);
            if (jobTitle?.Name == "مدير")
            {
                var existingManager = await _context.People
                    .FirstOrDefaultAsync(p => p.ProjectId == projectId && p.JobTitle.Name == "مدير");

                if (existingManager != null)
                {
                    TempData["Error"] = "لا يمكن إضافة أكثر من مدير لنفس المشروع.";
                    return RedirectToAction("Index");
                }
            }

            var person = new Person
            {
                Name = name,
                JobTitleId = jobTitleId,
                ProjectId = projectId
            };

            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
                return NotFound();

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
