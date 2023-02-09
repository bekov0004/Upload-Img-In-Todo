using Microsoft.AspNetCore.Http;

namespace Domain.Dtos;

public class AddTodoImages:TodoImagesDto
{
    public IFormFile File { get; set; }

}