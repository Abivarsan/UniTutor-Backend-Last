using Microsoft.EntityFrameworkCore;
using UniTutor.Data;
using UniTutor.Interface;
using UniTutor.Repository;
using UniTutor.Respository;
using UniTutor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var smtpSettings = builder.Configuration.GetSection("Smtpsettings").Get<Smtpsettings>() ?? throw new ArgumentNullException(nameof(Smtpsettings));

// Ensure Port is set to a default value if it's null
int port = smtpSettings.Port ?? 25; // Replace 25 with your default port value

builder.Services.AddSingleton(smtpSettings);

builder.Services.AddTransient<IEmailService>(provider =>
    new EmailService(
        smtpSettings.SmtpServer ?? throw new ArgumentNullException(nameof(smtpSettings.SmtpServer)),
        port,
        smtpSettings.Username ?? throw new ArgumentNullException(nameof(smtpSettings.Username)),
        smtpSettings.Password ?? throw new ArgumentNullException(nameof(smtpSettings.Password))
    ));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Configure database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add repositories
builder.Services.AddScoped<IAdmin, AdminRepository>();
builder.Services.AddScoped<ITutor, TutorRepository>();
builder.Services.AddScoped<IStudent, StudentRepository>();
builder.Services.AddScoped<ILastJoined, LastJoinedRepository>();
builder.Services.AddScoped<IAnalytics, AnalyticsRepository>();
builder.Services.AddScoped<ICurrentUsersTotal, CurrentUsersTotalRepository>();




// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "UniTutor API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniTutor API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
