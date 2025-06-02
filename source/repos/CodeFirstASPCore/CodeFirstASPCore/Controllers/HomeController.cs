using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudCodeFirstEF.Models;

namespace CrudCodeFirstEF.Controllers;

public class HomeController : Controller
{


    //private readonly ILogger<HomeController> _logger;
    //public HomeController(ILogger<HomeController> logger)
    //{ 
    //    _logger = logger;
    //}
    private readonly EmployeeDBContext employeeDB;
    public HomeController(EmployeeDBContext employeeDB)
    {
        this.employeeDB = employeeDB;
    }
    
    public IActionResult Index()
    {
        var employee=employeeDB.Employees.ToList();
        return View(employee);
    }

    public IActionResult Create()
    {
   
        return View();
    }

    [HttpPost]
    public IActionResult Create(Employee emp)
    {
        if (ModelState.IsValid)
        {
            employeeDB.Employees.Add(emp);
            employeeDB.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(emp);
    }

    public IActionResult Details(int? id)
    {
        if(id==null || employeeDB.Employees == null)
        {
            return NotFound();
        }
        var employee=employeeDB.Employees.FirstOrDefault(x=> x.Id == id);
        employeeDB.SaveChanges();
        return View(employee);
    }

    public IActionResult Edit(int? id)
    {
        if(id ==null || employeeDB.Employees == null)
        {
            return NotFound();
        }
        var employee= employeeDB.Employees.Find(id);
        return View(employee);
    }

    [HttpPost]
    public IActionResult Edit(int? id,Employee emp)
    {
        if (id == null || employeeDB.Employees == null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            employeeDB.Employees.Update(emp);
            employeeDB.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(emp);
    }


    public IActionResult Delete(int? id)
    {
       
        var emp= employeeDB.Employees.FirstOrDefault(x=> x.Id == id);
        return View(emp);
    }

    [HttpPost,ActionName("Delete")]
    public IActionResult DeleteConformed(int? id)
    {
        var empData = employeeDB.Employees.Find(id);
        if (empData != null)
        {
           employeeDB.Employees.Remove(empData);
        }
        employeeDB.SaveChanges();
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
