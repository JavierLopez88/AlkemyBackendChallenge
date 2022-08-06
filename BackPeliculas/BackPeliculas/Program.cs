using Microsoft.EntityFrameworkCore;
using BackPeliculas.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyección de dependencia
builder.Services.AddEntityFrameworkNpgsql()
.AddDbContext<Context>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDatabase")));

//------------------------------------------

//seguridad
builder.Services.AddCors();

var app = builder.Build();

//formateo de datos fecha:
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Cors que aceptará nuestra app:
app.UseCors(c  => {
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
