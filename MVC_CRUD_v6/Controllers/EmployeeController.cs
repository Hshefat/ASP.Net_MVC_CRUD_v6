using Microsoft.AspNetCore.Mvc;
using MVC_CRUD_v6.Models;

namespace MVC_CRUD_v6.Controllers
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }
    public class EmployeeController : Controller
    {
        HRDatabaseContext dbContext = new HRDatabaseContext();
        public IActionResult Index(string sortField, string currentSortField, SortDirection SortDirection)
        {
            var employees = GetEmployeesData();
            /*List<Employee> employees = dbContext.Employees.ToList(); */
            /*return GetEmployeesData();*/
            /* return View(employees);*/
            return View(this.SortEmployees(employees, sortField, currentSortField, SortDirection));
        }

        private List<Employee> GetEmployeesData()
        {
            var employees = (from employee in dbContext.Employees
                             join department in dbContext.Departments on
                             employee.Departmentid equals department.DepartmentId
                             select new Employee
                             {
                                 EmployeeId = employee.EmployeeId,
                                 EmployeeName = employee.EmployeeName,
                                 EmployeeNumber = employee.EmployeeNumber,
                                 DOB = employee.DOB,
                                 HiringDate = employee.HiringDate,
                                 GrossSalary = employee.GrossSalary,
                                 NetSalary = employee.NetSalary,
                                 Departmentid = employee.Departmentid,
                                 DepartmentName = department.DepartmentName,
                             }).ToList();
            return employees;
        }

        public IActionResult Create()
        {
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            ModelState.Remove("EmployeeId");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");

            if (ModelState.IsValid)
            {
                dbContext.Employees.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View();
        }

        public IActionResult Edit(int ID)
        {
            Employee data = this.dbContext.Employees.Where(e => e.EmployeeId == ID).FirstOrDefault();
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View("Create", data);
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            ModelState.Remove("EmployeeId");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");

            if (ModelState.IsValid)
            {
                dbContext.Employees.Update(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View("Create", model);
        }
        public IActionResult Delete(int Id)
        {
            Employee data = this.dbContext.Employees.Where(e => e.EmployeeId == Id).FirstOrDefault();
            if (data != null)
            {
                dbContext.Employees.Remove(data);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        private List<Employee> SortEmployees(List<Employee> employees, string sortField, string currentSortField, SortDirection sortDirection)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                ViewBag.SortField = "EmployeeNumber";
                /*ViewBag.sortDirection = "Asc";*/
                ViewBag.sortDirection = SortDirection.Ascending;

            }
            else
            {
                if (currentSortField == sortField)
                    ViewBag.sortDirection = sortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
                else
                    ViewBag.SortDirection = SortDirection.Ascending;
                ViewBag.SortField = sortField;
            }
            var propertyInfo = typeof(Employee).GetProperty(ViewBag.sortField);
            if (ViewBag.SortDirection == SortDirection.Ascending)
                employees = employees.OrderByDescending(e => propertyInfo.GetValue(e, null)).ToList();
            return employees;
        }

    }
}
