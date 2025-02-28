using Microsoft.EntityFrameworkCore;

namespace Mission08_0110.Models;

public class EFTaskRepository : ITaskRepository
{
    private TaskContext _context;

    public EFTaskRepository(TaskContext temp)
    {
        _context = temp;
    }

    public IQueryable<Task> Tasks => _context.Tasks.Include(t => t.Category);
    public IQueryable<Category> Categories => _context.Categories;

    public void AddTask(Task task)
    {
        _context.Add(task);
        _context.SaveChanges();
    }

    public void UpdateTask(Task task)
    {
        _context.Update(task);
        _context.SaveChanges();
    }

    public void DeleteTask(Task task)
    {
        _context.Remove(task);
        _context.SaveChanges();
    }
}