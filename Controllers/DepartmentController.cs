using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsEnrollment.Data;
using StudentsEnrollment.Data.Entities;
using StudentsEnrollment.Models;

namespace StudentsEnrollment.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _application;

        public DepartmentController(ApplicationDbContext a)
        {
            _application = a;
        }
        public ActionResult Index()
        {
            var model = _application.Departments.Select(dp => new DepModel
            {
                Name = dp.DepartmentName,
                Count = dp.StudentCount,
                ID = dp.Id
            }).ToList();


            return View(model);
        }

        public IActionResult Delete(int id)
        {
            
            List<string> list = new List<string>();

            foreach(Department d in _application.Departments)
            {
                if (d.Id == id)
                    continue;
                list.Add(d.DepartmentName);
            }

            DepListModel model = new DepListModel
            {
                DeletedDep = new DepModel { ID = id },
                ExistingDepsNames = list
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(DepListModel model, string deletionOption)
        {
            var dep = _application.Departments.FirstOrDefault(d => d.Id == model.DeletedDep.ID);
            model.DeletedDep.Name = dep.DepartmentName;

            if(deletionOption == "relocate")
            {
                
                foreach (Student student in _application.Students)
                {
                    if (student.Department.DepartmentName == model.DeletedDep.Name)
                    {
                        //return Json(new { isValueValid = deletionOption });
                        var d = _application.Departments
                                            .FirstOrDefault(item => item.DepartmentName == model.ChangedDepName);
                        student.Department = d;
                        _application.Entry(student).State = EntityState.Modified;
                    }
                        
                }
            }

            _application.Departments.Remove(dep);
            _application.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            DepModel model = new DepModel();

            return View(model);
        }

        [HttpPost]

        public IActionResult Add(DepModel model)
        {
            if (ModelState.IsValid)
            {
                foreach(Department d in _application.Departments)
                {
                    if (d.DepartmentName == model.Name)
                        return RedirectToAction("Index");
                }
                Department dep = new Department
                {
                    DepartmentName = model.Name,
                };

                _application.Departments.Add(dep);
                _application.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
