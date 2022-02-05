using Book_Shop.Web;

var startup = new Startup();
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseSentry(o =>
{
    o.Dsn = "https://ec7ca54ed4b34cf0a0ff37a76761dde8@o1110448.ingest.sentry.io/6139487";
    o.Debug = true;
    o.TracesSampleRate = 1.0;
});
startup.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
startup.Configure(app, builder.Environment);

app.Run();
