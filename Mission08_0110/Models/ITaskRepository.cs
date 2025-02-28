namespace Mission08_0110.Models;

public interface ITaskRepository
{
    IQueryable<Task> Tasks { get;}
    IQueryable<Category> Categories { get; }

    public void AddTask(Task task);

    public void UpdateTask(Task task);

    public void DeleteTask(Task task);
}