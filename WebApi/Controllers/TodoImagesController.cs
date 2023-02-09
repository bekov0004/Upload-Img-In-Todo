using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]
public class TodoImagesController:ControllerBase
{
    private readonly ITodoServices<TodoImagesDto,AddTodoImages> _todoServices;
    private readonly IWebHostEnvironment _environment;

    public TodoImagesController(ITodoServices<TodoImagesDto,AddTodoImages> todoServices, IWebHostEnvironment environment)
    {
        _todoServices = todoServices;
        _environment = environment;
    }

    [HttpGet("GetTodoImg")]
    public async Task<Response<List<TodoImagesDto>>> GetTodoImg()
    {
       return await _todoServices.GetTodo();
    }
    
    [HttpPost("AddTodoImg")]
    public async Task<Response<TodoImagesDto>> AddTodoImg(AddTodoImages addTodoImages)
    {
        return await _todoServices.AddTodo(addTodoImages);
    }
    
    [HttpPut("UpdateTodoImg")]
    public async Task<Response<TodoImagesDto>> UpdateTodoImg(AddTodoImages addTodoImages)
    {
        return await _todoServices.UpdateTodo(addTodoImages);
    }

    [HttpDelete("DeleteTodoImg")]
    public async Task<Response<string>> Deleted(int id)
    {
       return await _todoServices.DeleteTodo(id);
    }
    
    
}