using WebApi.Data;
using WebApi.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(); // add view engine
builder.Services.AddDbContext<SqliteDbContext>();

// read appsettings.json using options https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0
builder.Services.Configure<EmailOptions>(
    builder.Configuration.GetSection(EmailOptions.DefaultEmail));

// Cross-Origin Resource Sharing (i.e. calling the API from a different domain and/or port, for example Postman)
builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global Cross-Origin Resource Sharing policy:
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//app.UseHttpsRedirection();
//app.UseAuthorization();

app.MapControllers();

// Not relevant, but check curiosity on why/how specifying the port doesn't work
app.Run(); // Adding a run URL seems to break something for API testing, not sure why: app.Run("http://localhost:xxxx");


//##• Code should be written in C#.
//##• Send Email Method should be in a dll that can be reused throughout different applications and
//entry points.
//• Email sender, recipient, subject, and body (not attachments), and date must be logged/stored
//indefinitely with status of send attempt.
//##• If email fails to send it should either be retried until success or a max of 3 times whichever
//comes first, and can be sent in succession or over a period of time.
//##• Please store all credentials in an appsettings instead of hardcoded.
//##• At minimum that method/dll should be called from a console application.
//##• Extra Credit if attached to an API that can be called from Postman.
//##• EXTRA Credit if a front end (wpf/asp.net web application/etc...) calls the API to send the email.
//##• In any scenario you should be able to take in an input of a recipient email to send a test email.