namespace Domain.Entities;

public class TodoImages
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public int TodoId { get; set; }
    
    public Todo Todo { get; set; }
}