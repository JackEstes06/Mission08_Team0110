using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_0110.Models;
using Task = Mission08_0110.Models.Task;

namespace Mission08_0110.Controllers;

public class HomeController : Controller
{
    private TaskContext _context;

    public HomeController(TaskContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult AddTask()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View(new Task());
    }
    [HttpPost]
    public IActionResult AddTask(Task task)
    {
        if (ModelState.IsValid)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();

            return RedirectToAction("Quadrants");
        }
        else
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(task);
        }
    }
    
    [HttpGet]
    public IActionResult Quadrants()
    {
        var tasks = _context.Tasks
            .Include(x => x.Category)
            .Where(x=>x.Completed == false)
            .ToList();
        return View("Quadrants", tasks);
    }

    [HttpPost]
    public IActionResult Quadrants(Task updated)
    {
        _context.Update(updated);
        _context.SaveChanges();
        return RedirectToAction("Quadrants");
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        Task taskToEdit = _context.Tasks
            .Single(x => x.TaskId == id);
        
        ViewBag.Categories = _context.Categories.ToList();
        return View("AddTask", taskToEdit);
    }
    [HttpPost]
    public IActionResult Edit(Task task)
    {
        _context.Update(task);
        _context.SaveChanges();
        
        return RedirectToAction("Quadrants");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Task taskToDelete = _context.Tasks
            .Single(x => x.TaskId == id);
        
        return View(taskToDelete);
    }
    [HttpPost]
    public IActionResult Delete(Task task)
    {
        _context.Tasks.Remove(task);
        _context.SaveChanges();
        
        return RedirectToAction("Quadrants");
    }
}