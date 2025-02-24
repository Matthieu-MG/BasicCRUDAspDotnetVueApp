using Enterprise.API.Validators;
using Enterprise.Data;
using Enterprise.Services.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors( options =>
{
    options.AddPolicy("AllowFrontend", policy => 
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

//* Validator services
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//* Adds Only Controllers and DateOnly converters for Web API
builder.Services.AddControllers()
                .AddJsonOptions( opt => opt.JsonSerializerOptions.AddDateOnlyConverters() );;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlConnection = builder.Configuration.GetConnectionString("Development");

builder.Services.AddDbContext<EnterpriseDbContext>(options =>
{
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection));
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<QuotationRepository>();
builder.Services.AddTransient<OrderRepository>();
builder.Services.AddTransient<ProductRepository>();
builder.Services.AddTransient<SocietyRepository>();
builder.Services.AddTransient<EmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
