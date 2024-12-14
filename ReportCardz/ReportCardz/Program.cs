using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ReportCardz.Components;
using ReportCardz.Persistence;
using ReportCardz.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
var mongoConnectionString = builder.Configuration.GetConnectionString("ReportCardsUri");
var mongoClient = new MongoClient(mongoConnectionString);
var db = ReportCardsDbContext.Create(mongoClient.GetDatabase("reportcards"));
db.Database.EnsureDeleted();
db.Database.EnsureCreated();
db.Children.AddRange([
    new Child
    {
        Name = "Richard",
        Courses = new Dictionary<string, char>()
        {
            { "Math", 'A' },
            { "Science", 'A' },
            { "Spanish", 'A' }

        }
    },

    new Child
    {
        Name = "Trinity",
        Courses = new Dictionary<string, char>()
        {
            { "Math", 'A' },
            { "Science", 'A' },
            { "Chinese", 'A' }

        }
    }

]);
db.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
db.SaveChanges();
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddScoped<IReportCardsRepository>( p =>  new ReportCardsRepository(db));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();