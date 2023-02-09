using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class TodoDto
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
}