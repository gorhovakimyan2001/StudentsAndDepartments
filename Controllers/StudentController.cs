using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StudentsEnrollment.Data;
using StudentsEnrollment.Data.Entities;
using StudentsEnrollment.Models;


namespace StudentsEnrollment.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _application;

        public StudentController(ApplicationDbContext a)
        {
            _application = a;
        }
        public IActionResult Index()
        {
            var students = _application.Students.Select(student => new StudentModel
            {
                ID = student.Id,
                Name = student.StudentName,
                Surname = student.StudentSurname,
                Age = student.Age,
                Department = student.Department.ToString()

            }).ToList();

            return View(students);
        }

        public IActionResult Add()
        {
            DepNameListAndStudentModel model;
            
            List<string> departments = new List<string>();
            foreach(Department d in _application.Departments)
            {
                departments.Add(d.DepartmentName);
            }
            model = new DepNameListAndStudentModel()
            {
                student = new StudentModel(),
                departmnetsNames = departments
            };

            
            return View(model);
        }

        [HttpPost]

        public IActionResult Add(DepNameListAndStudentModel model)
        {
            
            if (ModelState.IsValid)
            {
                Student student = new Student
                {
                    StudentName = model.student.Name,
                    StudentSurname = model.student.Surname,
                    Age = model.student.Age,
                };

                foreach (Department item in _application.Departments)
                {
                    if (item.DepartmentName == model.student.Department)
                    {
                        student.Department = item;
                        break;
                    }

                }

                _application.Students.AddAsync(student);
                _application.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            DepNameListAndStudentModel model;

            var student = _application.Students.Include(s => s.Department).FirstOrDefault(b => b.Id == id);

            if (student == null)
            {

                return NotFound(); 
            }

            StudentModel s = new StudentModel
            {
                Name = student.StudentName,
                Age = student.Age,
                Surname = student.StudentSurname,
                ID = student.Id,
                Department = student.Department.ToString()
            };

            List<string> l = new List<string>();
            foreach (Department d in _application.Departments)
                l.Add(d.DepartmentName);

            model = new DepNameListAndStudentModel
            {
                student = s,
                departmnetsNames = l
            };
            

            return View(model);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        
        public ActionResult Edit(DepNameListAndStudentModel model)
        {
            StudentModel st = model.student;

            var student = _application.Students.FirstOrDefault(s => s.Id == st.ID);


            if (student != null)
            {
                student.StudentName = st.Name;
                student.StudentSurname = st.Surname;
                student.Age = st.Age;
            }

            
            foreach (Department item in _application.Departments)
            {
               
                if (item.DepartmentName == st.Department)
                {
                    
                    student.Department = item;
                    
                    break;
                }
            }

            _application.Entry(student).State = EntityState.Modified;
            _application.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            
            try
            {
      
                var student = _application.Students.Include(s=> s.Department)
                                                   .FirstOrDefault(b => b.Id == id);
                if (student != null)
                {
                    _application.Students.Remove(student);
                    _application.SaveChanges();
                    
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Student not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

     }
}
