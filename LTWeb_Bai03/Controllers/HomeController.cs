using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTWeb_Bai03.Models;

namespace LTWeb_Bai03.Controllers
{
    public class HomeController : Controller
    {
        private QLNhanSu db = new QLNhanSu();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult EmployeeList()
        {
            var employees = db.Employee.Include("Department").ToList();
            return View(employees);
        }

        // GET: Create
        public ActionResult Create()
        {
            ViewBag.DeptId = new SelectList(db.Department, "DeptId", "Name");
            ViewBag.Gender = new SelectList(new[] { "Nam", "Nữ" });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    string fileName = System.IO.Path.GetFileName(Image.FileName);
                    string folderPath = Server.MapPath("~/Content/Images/");
                    string fullPath = System.IO.Path.Combine(folderPath, fileName);

                    Image.SaveAs(fullPath);

                    emp.Img = "/Content/Images/" + fileName;
                }

                db.Employee.Add(emp);
                db.SaveChanges();
                return RedirectToAction("EmployeeList");
            }

            ViewBag.DeptId = new SelectList(db.Department, "DeptId", "DeptName", emp.DeptId);
            ViewBag.Gender = new SelectList(new[] { "Nam", "Nữ" }, emp.Gender);
            return View(emp);
        }

        // GET: Edit
        public ActionResult Edit(int id)
        {
            var emp = db.Employee.Find(id);
            if (emp == null)
            {
                return HttpNotFound();
            }

            ViewBag.DeptId = new SelectList(db.Department, "DeptId", "Name", emp.DeptId);
            ViewBag.Gender = new SelectList(new[] { "Nam", "Nữ" }, emp.Gender);
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                var existingEmp = db.Employee.Find(emp.Id);
                if (existingEmp == null)
                {
                    return HttpNotFound();
                }

                existingEmp.Name = emp.Name;
                existingEmp.Gender = emp.Gender;
                existingEmp.City = emp.City;
                existingEmp.DeptId = emp.DeptId;

                if (Image != null && Image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Image.FileName);
                    string folderPath = Server.MapPath("~/Content/Images/");
                    string fullPath = Path.Combine(folderPath, fileName);
                    Image.SaveAs(fullPath);
                    existingEmp.Img = "/Content/Images/" + fileName;
                }

                db.SaveChanges();
                return RedirectToAction("EmployeeList");
            }

            ViewBag.DeptId = new SelectList(db.Department, "DeptId", "Name", emp.DeptId);
            ViewBag.Gender = new SelectList(new[] { "Nam", "Nữ" }, emp.Gender);
            return View(emp);
        }


        // GET: DELETE
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var emp = db.Employee.Find(id);
            if (emp != null)
            {
                db.Employee.Remove(emp);
                db.SaveChanges();
            }

            return RedirectToAction("EmployeeList");
        }


        // GET: Details
        public ActionResult Details(int id)
        {
            var emp = db.Employee.Include("Department").FirstOrDefault(e => e.Id == id);

            if (emp == null)
            {
                return HttpNotFound();
            }

            return View(emp);
        }

        public PartialViewResult DepartmentMenu()
        {
            var depts = db.Department.ToList();
            return PartialView("_DepartmentMenu", depts);
        }

        public ActionResult EmployeeByDept(int id)
        {
            var employees = db.Employee.Include("Department")
                                       .Where(e => e.DeptId == id)
                                       .ToList();

            var dept = db.Department.Find(id);
            ViewBag.DeptName = dept != null ? dept.Name : "Không xác định";

            return View("EmployeeList", employees);
        }
    }
}