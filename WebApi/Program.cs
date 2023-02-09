using Domain.Dtos;
using Infrastructure.Data;
using Infrastructure.MapperProfiles;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(conf => conf.UseNpgsql(connection));
builder.Services.AddControllers();
builder.Services.AddScoped<IFileService, FileServices>(); 
builder.Services.AddScoped<ITodoServices<TodoImagesDto,AddTodoImages>, TodoImgService>(); 
builder.Services.AddScoped<ITodoServices<TodoDto,AddTodoDto>, TodoService>(); 

//configure automapper
builder.Services.AddAutoMapper(typeof(InfrastructureProfile));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.Run();
