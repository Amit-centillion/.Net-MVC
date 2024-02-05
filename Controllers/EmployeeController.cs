using ASPNETMVCCURUD.Data;
using ASPNETMVCCURUD.Models;
using ASPNETMVCCURUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCURUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDemoDbContext mVCDemoDbContext;

        public EmployeeController(MVCDemoDbContext mVCDemoDbContext)
        {
            this.mVCDemoDbContext = mVCDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mVCDemoDbContext.Employees.ToListAsync();
            
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeReq)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = addEmployeeReq.Name,
                    Email = addEmployeeReq.Email,
                    Salary = addEmployeeReq.Salary,
                    Department = addEmployeeReq.Department,
                    DateOfBirth = addEmployeeReq.DateOfBirth,
                };

                await mVCDemoDbContext.Employees.AddAsync(employee);
                await mVCDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // If ModelState is not valid, redisplay the form with validation errors
            return View(addEmployeeReq);
        }

        [HttpGet]

        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mVCDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
          
            if(employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> view(UpdateEmployeeViewModel viewModel)
        {
            var employee = await mVCDemoDbContext.Employees.FindAsync(viewModel.Id);
            if(employee != null)
            {
                employee.Name = viewModel.Name;
                employee.Email = viewModel.Email;
                employee.Salary = viewModel.Salary;
                employee.Department = viewModel.Department;
                employee.DateOfBirth = viewModel.DateOfBirth;

                await mVCDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mVCDemoDbContext.Employees.FindAsync(model.Id);
            if(employee != null)
            {
                mVCDemoDbContext.Employees.Remove(employee);
                await mVCDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await mVCDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                mVCDemoDbContext.Employees.Remove(employee);
                await mVCDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
