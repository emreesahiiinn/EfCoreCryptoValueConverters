using Application.DependencyRegistration;
using Persistence.DependencyRegistration;

// Application startup and configuration
var builder = WebApplication.CreateBuilder(args);

// Register persistence layer services (DbContext, repositories)
builder.Services.AddPersistenceServices();

// Register application layer services (managers, business logic)
builder.Services.AddApplicationServices();

// Register MVC controllers
builder.Services.AddControllers();

// Configure CORS for development
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:5282", "https://localhost:5282").AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure HTTP request pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();