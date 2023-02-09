using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.MapperProfiles;

public class InfrastructureProfile:Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Todo,TodoDto>();
        CreateMap<AddTodoDto, Todo>();
        
        CreateMap<TodoImages,TodoImagesDto>();
        CreateMap<AddTodoImages, TodoImages>();
    }
    
}