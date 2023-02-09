using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TodoService:ITodoServices<TodoDto,AddTodoDto>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public TodoService(DataContext context, IMapper mapper, IFileService fileService)
    {
        _context = context;
        _mapper = mapper;
        _fileService = fileService;
    }

    public string name { get; set; }

    public async Task<Response<List<TodoDto>>> GetTodo()
    {
        var response = await _context.Todos.ToListAsync();
        var mapped = _mapper.Map<List<TodoDto>>(response);
        return new Response<List<TodoDto>>(mapped);
    }

    public async Task<Response<TodoDto>> AddTodo(AddTodoDto addTodoDto)
    {
        var mapped = _mapper.Map<Todo>(addTodoDto);
        await _context.Todos.AddAsync(mapped);
        await _context.SaveChangesAsync();
        return new Response<TodoDto>(addTodoDto);
    }

    public async Task<Response<TodoDto>> UpdateTodo(AddTodoDto updateTodoDto)
    {
        var mapped = _mapper.Map<Todo>(updateTodoDto);
        _context.Todos.Update(mapped);
        await _context.SaveChangesAsync();
        return new Response<TodoDto>(updateTodoDto);
    }


    public async Task<Response<string>> DeleteTodo(int id)
    {
        var ex2 = await _context.TodoImages.FindAsync(id);
        var existing =await _context.Todos.FindAsync(id);
        if (existing == null)
            return new Response<string>(HttpStatusCode.BadRequest, new List<string>() { "BadRequest" });
        _context.Todos.Remove(existing); 
        await _fileService.DeleteFile(ex2.FileName, "Images");
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted");
    }
}