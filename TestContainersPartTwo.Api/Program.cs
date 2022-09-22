using MediatR;
using Microsoft.Extensions.FileProviders;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using TestContainersPartTwo.Api.Shared.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Database
builder.Services.AddScoped<IDbConnection, SqlConnection>(ctx =>
{
    var connection = new SqlConnection(builder.Configuration.GetConnectionString("WeatherDbConnection"));
    return connection;
});
builder.Services.AddScoped<IDatabase, WeatherDatabase>();
var applicationAssembly = Assembly.GetAssembly(typeof(EmbeddedFileReader));
var embeddedProvider = new ManifestEmbeddedFileProvider(applicationAssembly!);
builder.Services.AddSingleton<IFileProvider>(embeddedProvider);
builder.Services.AddSingleton<IEmbeddedFileReader, EmbeddedFileReader>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

// Changing the implicit access modifier of "internal" to "public" for WebApplicationFactory based integration testing
// More info, here: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
public partial class Program { }