using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudWithoutDataBase.Models;

namespace CrudWithoutDataBase.Controllers;

public class HomeController : Controller
{
    private static List<Employee> employees = new List<Employee>()
    {
        new Employee(){Id=1,Name="Sanket", Designation=".Net Developer"},
        new Employee(){Id=2,Name="Vaibhav", Designation="Java Developer"}
    };

    //private static int employeeCounter = 1;

    //private readonly ILogger<HomeController> _logger;

    //public HomeController(ILogger<HomeController> logger)
    //{
    //    _logger = logger;
    //}

    public IActionResult Index()
    {

        return View(employees);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create( Employee employee)
    {
        //Console.WriteLine("Employee Name :"+ employee.Name);
        //Console.WriteLine("Employee Description  : " + employee.Designation);

        if (ModelState.IsValid)
        {
            //employee.Id = employeeCounter++;

            employees.Add(employee);

            return RedirectToAction("Index");
        }

        return View(employee);
    }

    [HttpGet]
    //[Route("Home/Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null) return NotFound();
        return View(employee);
    }

    [HttpPost]
    public IActionResult Edit(Employee employee)
    {
        var existingEmployee=employees.FirstOrDefault(e=>e.Id == employee.Id);
        if (existingEmployee == null) return NotFound();
        existingEmployee.Id = employee.Id;
        existingEmployee.Name = employee.Name;
        existingEmployee.Designation = employee.Designation;
       
        return RedirectToAction("Index");

    }

    
    public IActionResult Details(int id)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null) return NotFound();
        return View(employee);
    }

    public IActionResult Delete(int? id)

    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null) return NotFound();
        
        return View("Index");
    }

    [HttpPost ,ActionName("Delete")]
    public IActionResult DeleteConfirmed(int? id)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee != null) return NotFound();
       
            employees.Remove(employee);

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
