using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_0110.Models;
using SQLitePCL;
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

        _repo.Tasks.Add(task);
        _repo.SaveChanges();

        return RedirectToAction("Quadrants");
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