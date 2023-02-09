namespace Domain.Entities;

public class Todo 
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<TodoImages> TodoImages { get; set; }
}