using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TodoImgService:ITodoServices<TodoImagesDto,AddTodoImages>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public TodoImgService(DataContext context, IMapper mapper, IFileService fileService)
    {
        _context = context;
        _mapper = mapper;
        _fileService = fileService;
    }

    public string name { get; set; }

    public async Task<Response<List<TodoImagesDto>>> GetTodo()
    {
        var response = await _context.TodoImages.ToListAsync();
        var mapped = _mapper.Map<List<TodoImagesDto>>(response);
        return new Response<List<TodoImagesDto>>(mapped);
    }

    public async Task<Response<TodoImagesDto>> AddTodo(AddTodoImages addTodoImages)
    {
        var mapped = _mapper.Map<TodoImages>(addTodoImages);
        mapped.FileName = await _fileService.AddFile(addTodoImages.File.FileName, "Images", addTodoImages.File);
        await _context.TodoImages.AddAsync(mapped);
        await _context.SaveChangesAsync();
        addTodoImages.Id = mapped.Id;
        return new Response<TodoImagesDto>(addTodoImages);
    }

    public async Task<Response<TodoImagesDto>> UpdateTodo(AddTodoImages updateTodoImages)
    {
        var existing = await _context.TodoImages.FindAsync(updateTodoImages.Id);
        if(existing == null) return new Response<TodoImagesDto>(HttpStatusCode.NotFound,new List<string>(){$"Not found"});
        existing.Id = updateTodoImages.Id;
        existing.FileName = updateTodoImages.FileName;
        existing.TodoId = updateTodoImages.TodoId;

        if (updateTodoImages.File != null)
        {
            if (existing.FileName != null)
            {
                await _fileService.DeleteFile(existing.FileName, "Images");
                existing.FileName = await _fileService.AddFile(updateTodoImages.File.FileName, "Images", updateTodoImages.File);
            }
            else
            {
                existing.FileName = await _fileService.AddFile(updateTodoImages.File.FileName, "Images", updateTodoImages.File);
            }
        }
        await _context.SaveChangesAsync();
        return new Response<TodoImagesDto>(updateTodoImages);
    }

    public async Task<Response<string>> DeleteTodo(int id)
    {
        var existing = await _context.TodoImages.FindAsync(id);
        if(existing == null) return new Response<string>(HttpStatusCode.NotFound,new List<string>(){$"Not found"});
        _context.TodoImages.Remove(existing);
        await _fileService.DeleteFile(existing.FileName, "Images");
        await _context.SaveChangesAsync();
        return new Response<string>($"Deleted");
    }
}