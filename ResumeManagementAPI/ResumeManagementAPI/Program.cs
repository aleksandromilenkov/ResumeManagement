using Microsoft.EntityFrameworkCore;
using ResumeManagementAPI.Data;
using ResumeManagementAPI.Profiles;

var builder = WebApplication.CreateBuilder(args);

// DB Configuration
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("resumeManagementConnection"),
    sqlServerOption => sqlServerOption.EnableRetryOnFailure()));
builder.Services.AddAutoMapper(typeof(MappingProfiles));
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
