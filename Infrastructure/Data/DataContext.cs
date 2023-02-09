using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

     public DbSet<TodoImages> TodoImages { get; set; }
     public DbSet<Todo> Todos { get; set; }
     
     
    
}