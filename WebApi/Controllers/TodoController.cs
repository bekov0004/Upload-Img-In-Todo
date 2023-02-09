using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]
public class TodoController:ControllerBase
{
    private readonly ITodoServices<TodoDto,AddTodoDto> _todoServices;
    private readonly IWebHostEnvironment _environment;

    public TodoController(ITodoServices<TodoDto,AddTodoDto> todoServices, IWebHostEnvironment environment)
    {
        _todoServices = todoServices;
        _environment = environment;
    }

    public string name { get; set; }
    [HttpGet("GetRootPath")]
    public string WwwRoot()
    {
        return _environment.WebRootPath;
        
    }

    [HttpGet("GetTodo")]
    public async Task<Response<List<TodoDto>>> GetTodo()
    {
        return await _todoServices.GetTodo();
    }

    [HttpPost("AddTodo")]
    public async Task<Response<TodoDto>> AddTodo([FromForm] AddTodoDto addTodoDto)
    {
      return await _todoServices.AddTodo(addTodoDto);
    }

    [HttpPut("UpdateTodo")]
    public async Task<Response<TodoDto>> UpdateTodo([FromForm] AddTodoDto addTodoDto)
    {
        return await _todoServices.UpdateTodo(addTodoDto);
    }

    [HttpDelete("DeleteTodo")]
    public async Task<Response<string>> DeleteTodo(int id)
    {
        return await _todoServices.DeleteTodo(id);
    }

}