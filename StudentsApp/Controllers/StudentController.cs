using Microsoft.AspNetCore.Mvc;
using StudentsApp.Models;
using StudentsApp.Service.Interfaces;

namespace StudentsApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository StudentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            StudentRepository = studentRepository;
        }

        public async Task<IActionResult> GetAllStudents()
        {
            var students = await StudentRepository.GetAll();
            return View(students);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var student = await StudentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentRepository.Update(student);
                await StudentRepository.SaveChanges();
                return RedirectToAction("GetAllStudents");
            }

            return View(student);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {
            if (ModelState.IsValid)
            {
                await StudentRepository.Add(student);
                await StudentRepository.SaveChanges();
                var studentByCode = StudentRepository.GetByCode(student.Code);
                return RedirectToAction("AddStudentSubjects", "StudentSubject", new { id = studentByCode.Id });
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid studentId)
        {
            var student = await StudentRepository.GetById(studentId);
            if (student == null)
            {
                return NotFound();
            }
            student.IsDeleted = true;
            StudentRepository.Update(student);
            await StudentRepository.SaveChanges();
            return Ok();
        }
    }
}
