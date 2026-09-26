using API.Modules;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddModule<CoreModule>(builder.Configuration);
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();