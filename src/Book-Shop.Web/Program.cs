using Book_Shop.Web;

var startup = new Startup();
var builder = WebApplication.CreateBuilder(args);
startup.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
startup.Configure(app, builder.Environment);

app.Run();
