using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Helpers;
using Cinema.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Cinema.Core.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Cinema.Core.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(CinemaProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TicketDTOValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SaleDTOValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserDTOValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserDTOValidator>());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();
app.MapControllers();
app.Run();