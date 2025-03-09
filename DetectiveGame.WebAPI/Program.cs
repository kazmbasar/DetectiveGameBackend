using DetectiveGame.Application.Behaviors;
using DetectiveGame.Application.Features.Games.Commands;
using DetectiveGame.Application.Mapping;
using DetectiveGame.Application.Repositories;
using DetectiveGame.Persistence.Contexts;
using DetectiveGame.Persistence.Repositories;
using DetectiveGame.WebAPI.Hubs;
using DetectiveGame.WebAPI.Middleware;

using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);   

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:3000"); // Frontend URL'inizi buraya ekleyin
        });
});

// Add SignalR services
builder.Services.AddSignalR();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateGameCommand).Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

// Add Validators
builder.Services.AddValidatorsFromAssembly(typeof(CreateGameCommandValidator).Assembly);

// Add DbContext
builder.Services.AddDbContext<DetectiveGameDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IEvidenceRepository, EvidenceRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Configure endpoints
app.MapHub<GameHub>("/gamehub");

app.Run();


