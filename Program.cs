using System.Collections.Generic;
using BasicAPI.Models;

// builder
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//list
List<Tasks> tasks = new List<Tasks>();
int nextId = 1;

app.MapGet("/tasks", () => tasks);

app.MapPost("/tasks", (Tasks task) =>
{
    if (string.IsNullOrEmpty(task.Name))
    {
        return Results.BadRequest();
    }

    if (task.Name.Length < 3)
    {
        return Results.BadRequest("name is too short!");
    }

    task.Id = nextId;
    nextId++;

    tasks.Add(task);

    return Results.Ok($"{task} successfully added");
});

app.Run();