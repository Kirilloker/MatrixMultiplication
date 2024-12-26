using MatrixMultiplication.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddRazorPages();

builder.Services.AddTransient<MatrixMultiplicationService>();
builder.Services.AddTransient<BlockMatrixMultiplicationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error");

app.UseStaticFiles();

app.MapRazorPages();

app.Run();
