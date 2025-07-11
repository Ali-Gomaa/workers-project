using Microsoft.AspNetCore.Mvc;
using Workers.Data;
using Workers.Models;

namespace Workers.Controllers
{
    public class JobTitlesController : Controller
    {
        private readonly AppDbContext _context;

        public JobTitlesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return RedirectToAction("Index", "Projects");

            if (_context.JobTitles.Any(j => j.Name == name))
            {
                TempData["Error"] = "المسمى الوظيفي موجود مسبقًا.";
                return RedirectToAction("Index", "Projects");
            }

            _context.JobTitles.Add(new JobTitle { Name = name });
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Projects");
        }
    }

}
