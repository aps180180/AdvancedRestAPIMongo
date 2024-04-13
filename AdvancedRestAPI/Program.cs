using AdvancedRestAPI.Data;
using AdvancedRestAPI.Interfaces;
using AdvancedRestAPI.Models;
using AdvancedRestAPI.Profiles;
using AdvancedRestAPI.Services;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData( option => option.Select().Filter().OrderBy());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Database Services
builder.Services.AddDbContext<AppDbContext>(options =>
{

    //options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

});
#endregion

#region DI Services
builder.Services.AddAutoMapper(typeof(AutomapperConfig));
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>((options) =>
{
    options.GeneralRules = new List<RateLimitRule>()
    {
        new RateLimitRule()
        {
            Endpoint ="*",
            Limit = 10,
            Period = "3m"       

        }
    };
});
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
#endregion


var app = builder.Build();
app.UseIpRateLimiting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
