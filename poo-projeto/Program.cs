var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var teste = "Ricardo";

app.MapGet("/", () => "Hello World! cacaro");

var teste = "pedro";

app.Run();
