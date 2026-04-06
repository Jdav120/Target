var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Bind to port 5000 for Elastic Beanstalk (nginx reverse proxies 80 -> 5000)
builder.WebHost.UseUrls("http://0.0.0.0:5000");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Health check endpoint for Elastic Beanstalk (do not remove)
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
