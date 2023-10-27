using Microsoft.AspNetCore.Mvc;
using StudentsApp.Models;
using StudentsApp.Service.Classes;
using StudentsApp.Service.Interfaces;

namespace StudentsApp.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository SubjectRepository;
        public SubjectController(ISubjectRepository subjectRepository)
        {
            SubjectRepository = subjectRepository;
        }

        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await SubjectRepository.GetAll();
            return View(subjects);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var subject = await SubjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Subject subject)
        {
            if (ModelState.IsValid)
            {
                SubjectRepository.Update(subject);
                await SubjectRepository.SaveChanges();
                return RedirectToAction("GetAllSubjects");
            }

            return View(subject);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await SubjectRepository.Add(subject);
                await SubjectRepository.SaveChanges();
                return RedirectToAction("GetAllSubjects");
            }
            return View(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid subjectId)
        {
            var subject = await SubjectRepository.GetById(subjectId);
            if (subject == null)
            {
                return NotFound();
            }
            subject.IsDeleted = true;
            SubjectRepository.Update(subject);
            await SubjectRepository.SaveChanges();
            return Ok();
        }
    }
}
