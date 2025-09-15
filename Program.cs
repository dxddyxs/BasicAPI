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

app.MapGet("/tasks/search", (string name) =>
{
    if (string.IsNullOrEmpty(name))
    {
        return Results.BadRequest();
    }
    //search for tasks that contain the term (case sensitive / ignorando maiusculas e minusculas) 
    var foundTasks = tasks.Where(t => t.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

    // results
    return Results.Ok(foundTasks);
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

app.MapGet("/tasks/status/{isComplete}", (bool isComplete) =>
{
    // Filtra as tarefas pelo status,  where = retorna uma lista
    var filteredItems = tasks.Where(t => t.Done == isComplete).ToList();

    return Results.Ok(filteredItems);
});

app.MapGet("/tasks/count/{isComplete}", (bool isComplete) =>
{
    var count = tasks.Count(t => t.Done == isComplete);

    return Results.Ok(new { count = count });
});

app.MapPut("/tasks/{id}", (int id, Tasks taskAtt) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task == null)
    {
        return Results.NotFound();
    }

    task.Name = taskAtt.Name;
    task.Description = taskAtt.Description;
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

app.MapDelete("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task == null)
    {
        return Results.NotFound();
    }

    tasks.Remove(task);

    return Results.Ok($"Removed!");
});

app.Run();