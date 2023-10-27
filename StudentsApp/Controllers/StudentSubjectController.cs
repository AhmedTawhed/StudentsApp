using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsApp.Models;
using StudentsApp.Service.Classes;
using StudentsApp.Service.Interfaces;
using StudentsApp.ViewModels;

namespace StudentsApp.Controllers
{
    public class StudentSubjectController : Controller
    {
        private readonly IStudentSubjectRepository StudentSubjectRepository;
        private readonly IStudentRepository StudentRepository;
        private readonly ISubjectRepository SubjectRepository;
        public StudentSubjectController(
            IStudentSubjectRepository studentSubjectRepository,
            IStudentRepository studentRepository,
            ISubjectRepository subjectRepository
            )
        {
            StudentSubjectRepository = studentSubjectRepository;
            StudentRepository = studentRepository;
            SubjectRepository = subjectRepository;
        }

        public async Task<IActionResult> GetAllStudentSubjects()
        {
            var StudentSubjects = await StudentSubjectRepository.GetAllWithDetails();
            return View(StudentSubjects);
        }
        public async Task<IActionResult> AddStudentsSubjects()
        {
            List<SelectListItem> students = (await StudentRepository.GetAll()).Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            List<SelectListItem> subjects = (await SubjectRepository.GetAll()).Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            ViewBag.students = students;
            ViewBag.subjects = subjects;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentsSubjects(IEnumerable<Guid> StudentIds, IEnumerable<Guid> SubjectIds)
        {
           await StudentSubjectRepository.AssignSubjectsToStudents(StudentIds, SubjectIds);

            return RedirectToAction("GetAllStudentSubjects");
        }

      
        public async Task<IActionResult> AddStudentSubjects(Guid id)
        {
            var student = await StudentRepository.GetById(id);

            List<SelectListItem> subjects = (await StudentSubjectRepository.GetSubjectsByStudentId(id)).Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            List<SelectListItem> allSubjects = (await SubjectRepository.GetAll()).Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            List<Guid> assignedSubjectIds = subjects.Select(s => Guid.Parse(s.Value)).ToList();
            List<SelectListItem> selectableSubjects = allSubjects.Where(s => !assignedSubjectIds.Contains(Guid.Parse(s.Value))).ToList();

            ViewBag.subjects = subjects;
            ViewBag.allSubjects = selectableSubjects; 

            StudentSubject model = new StudentSubject {StudentId = id, Student = student };
            return View(model);
        }
       
        [HttpPost]
        public async Task<IActionResult> AddStudentSubjects(StudentSubject studentSubject)
        {
            await this.StudentSubjectRepository.Add(studentSubject);
            await this.StudentSubjectRepository.SaveChanges();
            return RedirectToAction("AddStudentSubjects");

        }

        public async Task<IActionResult> AddStudentSubjectsGrades(Guid id)
        {
            var student = await StudentRepository.GetById(id);

            List<SelectListItem> subjects = (await StudentSubjectRepository.GetSubjectsByStudentId(id)).Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            ViewBag.subjects = subjects;
            StudentSubject model = new StudentSubject { StudentId = id, Student = student };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentSubjectsGrades(StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {
                var model = this.StudentSubjectRepository.Update(studentSubject);
                await this.StudentSubjectRepository.SaveChanges();
                return RedirectToAction("AddStudentSubjectsGrades", model.StudentId);
            }
            return View(studentSubject);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudentSubject(Guid StudentId, Guid SubjectId)
        {
            var studentSubject = await StudentSubjectRepository.GetById(StudentId, SubjectId);
            if (studentSubject != null)
            {
                StudentSubjectRepository.Delete(studentSubject);
                await StudentSubjectRepository.SaveChanges();
            }

            return RedirectToAction("AddStudentSubjects");
        }

        public async Task<IActionResult> Details(Guid Id)
        {
            var student = await StudentRepository.GetById(Id);

            var subjects = await StudentSubjectRepository.GetSubjectsByStudentId(Id);
            var allGrades = await StudentSubjectRepository.GetAll();

            var subjectIds = subjects.Select(s => s.Id).ToList();
            List<StudentSubject> stdGrades = new List<StudentSubject>();

            foreach (var subjectId in subjectIds)
            {
                var subjectGrade = allGrades.FirstOrDefault(s => s.SubjectId == subjectId && s.StudentId == Id);

                if (subjectGrade != null)
                {
                    stdGrades.Add(subjectGrade);
                }
            }

            ViewBag.subjects = subjects;
            ViewBag.grades = stdGrades;

            return View(student);
        }

        [HttpGet]
        public async Task<int> GetGrade(Guid studentId, Guid subjectId)
        {
            var studentSubject = await this.StudentSubjectRepository.GetById(studentId, subjectId);
            return studentSubject.Grade.GetValueOrDefault();
        }
    }
}
