using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodeFirstASPCore.Models;

namespace CodeFirstASPCore.Controllers;

public class HomeController : Controller
{


    //private readonly ILogger<HomeController> _logger;

    //public HomeController(ILogger<HomeController> logger)
    //{
    //    _logger = logger;
    //}
    private readonly StudentDBContext studentDB;

    public HomeController(StudentDBContext studentDB)
    {
        this.studentDB = studentDB;
    }

    
    public IActionResult Index()
    {
        var stddata =  studentDB.Students.ToList();
        return View(stddata);
    }

    [HttpGet] // This is needed to load the form
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Student std)
    {
        if (ModelState.IsValid)
        {
            studentDB.Students.Add(std);
            studentDB.SaveChanges();
           // TempData["insert_success"] = "Inserted";
            return RedirectToAction("Index","Home");
        }
        return View(std);
    }

    public IActionResult Details(int? id)
    {
        if(id == null || studentDB.Students == null)
        {
            return NotFound();
        }
        var stdData = studentDB.Students.FirstOrDefault(x => x.Id == id);
        return View(stdData);
    }

    public IActionResult Edit(int id)
    {
        var stddata = studentDB.Students.Find(id);
        return View(stddata);
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
