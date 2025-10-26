using EmailDeamon;
using EmailDeamon.Services;

var builder = Host.CreateApplicationBuilder(args);

// Configure settings
builder.Services.Configure<WorkerSettings>(
    builder.Configuration.GetSection("WorkerSettings"));

// Register StudentService
var connectionString = builder.Configuration.GetConnectionString("SchoolDatabase");
builder.Services.AddSingleton(new StudentService(connectionString!));

// Register the Worker
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();