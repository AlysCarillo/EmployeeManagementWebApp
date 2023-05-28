using EmployeeManagementWebApp.DbAccess;
using EmployeeManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDbContext _dbContext;

        public EmployeeController()
        {
            _dbContext = new EmployeeDbContext();
        }

        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> employees = _dbContext.GetAllEmployees();

            if(employees.Count == 0)
            {
                TempData["InfoMessage"] = "No Employees available in the Database.";
            }

            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var employee = _dbContext.GetEmployeeById(id);

            if (employee == null)
            {
                TempData["ErrorMessage"] = "Employee not found with ID " + id.ToString();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employee/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = _dbContext.InsertEmployee(employee);

                    if (id > 0)
                    {
                        TempData["SuccessMessage"] = "Employee has been successfully added!";
                    }
                    else if (id == -1)
                    {
                        TempData["ErrorMessage"] = "Employee Number already exists.";
                    }
                    else if (id == -2)
                    {
                        TempData["ErrorMessage"] = "Duplicate combination of First Name and Last Name is not allowed.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var employee  = _dbContext.GetEmployeeById(id);

            if(employee == null)
            {
                TempData["ErrorMessage"] = "Employee not found with ID " + id.ToString();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    bool isUpdated = _dbContext.UpdateEmployee(employee);
                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Employee has been successfully updated!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Employee was not updated";
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = _dbContext.GetEmployeeById(id);

            if (employee == null)
            {
                TempData["ErrorMessage"] = "Employee not found with ID " + id.ToString();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteNotification(int id)
        {
            try
            {
                string message = _dbContext.DeleteEmployee(id);
                if (message.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = message;
                }
                else
                {
                    TempData["ErrorMessage"] = message;
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }
    }
}
