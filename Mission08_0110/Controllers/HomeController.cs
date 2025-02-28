using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_0110.Models;
using Task = Mission08_0110.Models.Task;

namespace Mission08_0110.Controllers;

public class HomeController : Controller
{
    private ITaskRepository _repo;

    public HomeController(ITaskRepository temp)
    {
        _repo = temp;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult AddTask()
    {
        ViewBag.Categories = _repo.Categories.ToList();
        return View(new Task());
    }
    [HttpPost]
    public IActionResult AddTask(Task task)
    {
        // Debugging: Check validation errors
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);  // Log errors to console
            }
        }
        if (ModelState.IsValid)
        {
            _repo.AddTask(task);

            return RedirectToAction("Quadrants");
        }
        else
        {
            ViewBag.Categories = _repo.Categories.ToList();
            return View(task);
        }
    }
    
    [HttpGet]
    public IActionResult Quadrants()
    {
        var tasks = _repo.Tasks.ToList();
        return View("Quadrants", tasks);
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        Task taskToEdit = _repo.Tasks
            .Single(x => x.TaskId == id);
        
        ViewBag.Categories = _repo.Categories.ToList();
        return View("AddTask", taskToEdit);
    }
    [HttpPost]
    public IActionResult Edit(Task task)
    {
        _repo.UpdateTask(task);
        
        return RedirectToAction("Quadrants");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Task taskToDelete = _repo.Tasks
            .Single(x => x.TaskId == id);
        
        return View(taskToDelete);
    }
    [HttpPost]
    public IActionResult Delete(Task task)
    {
        _repo.DeleteTask(task);
        
        return RedirectToAction("Quadrants");
    }
}