using _00016339_Feedback_system.Data;
using _00016339_Feedback_system.Models;
using _00016339_Feedback_system.Repositories;
using _00016339_Feedback_system.Utils;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>

{

    options.AddPolicy(MyAllowSpecificOrigins,

               policy =>

               {

                   policy.WithOrigins("http://localhost:4200")

                           .AllowAnyHeader()

                           .AllowAnyMethod()

                           .AllowAnyOrigin();

               });

});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FeedbackDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

builder.Services.AddScoped<IRepository<Feedback>, FeedbackRepository>();
builder.Services.AddScoped<IRepository<Sender>, SenderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();