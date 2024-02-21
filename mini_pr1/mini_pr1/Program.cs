using Microsoft.EntityFrameworkCore;
using mini_pr1.Data;
using mini_pr1.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InputDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<InputDbContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//GetAllSchedule
app.MapGet("/api/Schedule", async (InputDbContext context) =>
{
        var schedules = await context.Schedule.ToListAsync();
        return schedules;
})
.WithName("GetSchedules")
.WithOpenApi();

//GetOneSchedule
app.MapGet("/api/Schedule/{id}", async (InputDbContext context, int id) =>
{
    var schedule = await context.Schedule.FindAsync(id);
    return schedule;
})
.WithName("GetSchedule")
.WithOpenApi();

//InsertOneSchedule
app.MapPost("/api/Schedule", async (InputDbContext context, Schedule addschedule) =>
{
    context.Schedule.Add(addschedule);
    await context.SaveChangesAsync();

    return (await context.Schedule.ToListAsync());
})
.WithName("PostSchedule")
.WithOpenApi();

//UpdateOneSchedule
app.MapPut("api/Schedule", async (InputDbContext context, Schedule updatedSchedule) =>
{
    var dbSchedule = await context.Schedule.FindAsync(updatedSchedule.Id);

    dbSchedule.Start = updatedSchedule.Start;
    dbSchedule.Destination = updatedSchedule.Destination;
    dbSchedule.RouteNumber = updatedSchedule.RouteNumber;
    dbSchedule.StartTime= updatedSchedule.StartTime;

    await context.SaveChangesAsync();

    return (await context.Schedule.ToListAsync());
})
.WithName("PutSchedule")
.WithOpenApi();

//DeleteOneSchedule
app.MapDelete("api/Schedule", async (InputDbContext context, int id) => {

    var dbSchedule = await context.Schedule.FindAsync(id);

    context.Schedule.Remove(dbSchedule);
    await context.SaveChangesAsync();

    return (await context.Schedule.ToListAsync());
})
.WithName("DeleteSchedule")
.WithOpenApi();

app.Run();

