using Microsoft.EntityFrameworkCore;
using TodoApp;
using TodoApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("TodoControllerLevel",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
    options.AddPolicy("SubTodoControllerLevel",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>
(o => o.UseInMemoryDatabase("TodoDatabase"));

var app = builder.Build();
//AddTodoData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors();


app.MapControllers();

app.Run();

//static void AddTodoData(WebApplication app)
//{
//    var scope = app.Services.CreateScope();
//    var db = scope.ServiceProvider.GetService<DBContext>();

//    var todo1 = new Todo
//    {
//        Id = 1,
//        Text = "Testing Todo",
//        MoreDetails = "Testing More Details",
//        DueDate = "12/20/2022",
//        Completed = true,
//    };

//    var subTodo1 = new SubTodo
//    {
//        Id = 1,
//        Text = "Testing Sub",
//        ParentID = 1,
//        Completed = false,
//    };

//    db.Todo.Add(todo1);

//    db.SubTodo.Add(subTodo1);

//    db.SaveChanges();
//}