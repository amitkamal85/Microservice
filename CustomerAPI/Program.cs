using Confluent.Kafka;
using CustomerAPI.Models;
using CustomerAPI.Repository;
using CustomerAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var producerConfig = new ProducerConfig();
builder.Configuration.Bind("producer", producerConfig);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ProducerConfig>(producerConfig);
builder.Services.AddControllers();
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
