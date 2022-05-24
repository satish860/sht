global using FastEndpoints;
using Google.Cloud.Firestore;
using UrlShortner.Web.Helper;
using UrlShortner.Web.Repository;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var url = $"http://0.0.0.0:{port}";

builder.Services.AddFastEndpoints();

builder.Services.AddScoped<IProjectProvider,ProjectProvider>();
IProjectProvider projectProvider = new ProjectProvider();
var projectId = projectProvider.GetProjectId();

builder.Services.AddSingleton<FirestoreDb>(FirestoreDb.Create(projectId));
builder.Services.AddScoped<IRepository,Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Hello World");

app.UseFastEndpoints();

app.Run(url);

