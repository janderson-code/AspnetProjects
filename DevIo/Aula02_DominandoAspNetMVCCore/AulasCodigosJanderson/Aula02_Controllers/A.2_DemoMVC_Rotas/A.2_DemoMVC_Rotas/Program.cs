var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "modulos",
    pattern: "Gestao/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "categoria",
    pattern: "{controller=Home}/{action=Index}/{id}/{categoria?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

//Abaixo N�o funciona mais 

//app.UseMvc(routes =>
//{
//    routes.MapRoute(
//    name: "categoria",
//    template: "Gestao/{controller=Home}/{action=Index}/{id}/{categoria?}");

//    routes.MapRoute(
//    name: "modulos",
//    template: "Gestao/{controller=Home}/{action=Index}/{id?}");
//});
