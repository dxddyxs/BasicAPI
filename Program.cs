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

    return Results.Created($"/tasks/{task.Id}", task);
});

app.MapGet("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(task);
});

app.MapPut("/tasks/{id}", (int id, Tasks taskAtt) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task == null)
    {
        return Results.NotFound();
    }

    task.Name = taskAtt.Name;
    task.Done = taskAtt.Done;

    return Results.Ok(task);
});

app.MapPatch("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task == null)
    {
        return Results.NotFound();
    }

    task.Done = true;

    return Results.Ok(task);
});

app.Run();