using UserManagementAPI.Application.Services;
using UserManagementAPI.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Register IUserRepository with FakeUserRepository
 builder.Services.AddSingleton<IUserRepository, FakeUserRepository>();

// Register IUserService with UserService
builder.Services.AddScoped<IUserService, UserService>();
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

app.Run();
