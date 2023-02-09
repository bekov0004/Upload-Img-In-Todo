using Domain.Dtos;
using Domain.Wrapper;

namespace Infrastructure.Services;

public interface ITodoServices<T,A>
{
    public string name { get; set; }
    Task<Response<List<T>>> GetTodo();
    Task<Response<T>> AddTodo(A name);
    Task<Response<T>> UpdateTodo(A name);
    Task<Response<string>> DeleteTodo(int id);
    
}