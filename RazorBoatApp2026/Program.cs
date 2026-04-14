using SailClubLibrary.Helpers.Filter;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IBoatRepository, BoatRepository>();//1. interfacetype 2. repository type - nu er boatrepo stillet til rådighed.
builder.Services.AddSingleton<IMemberRepository, MemberRepository>();
builder.Services.AddSingleton<IBookingRepository, BookingRepository>();
builder.Services.AddSingleton<IFilterFuncs,FilterFuncs>();
builder.Services.AddSingleton<IBoatRepoAsync, BoatRepositoryAsync>(); //Denne bruges ved async

builder.Services.AddSession(); // login
builder.Services.AddHttpContextAccessor(); //login

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession(); //login

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
