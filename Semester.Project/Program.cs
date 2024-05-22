using DataAccessLayer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Semester.Project.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<AdminService>();
builder.Services.AddSingleton<CatagoriesService>();
builder.Services.AddSingleton<AuthServices>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<BlogsService>();
builder.Services.AddSingleton<NewsLetterService>();
builder.Services.AddSingleton<CommentService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
