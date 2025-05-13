using System.Reflection;

using Auto_Circuit.Data;
using Auto_Circuit.Data.Repository;
using Auto_Circuit.Entities.identity;
using Auto_Circuit.Generics;
using Auto_Circuit.Interfaces;
using Auto_Circuit.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity<User, Role>(
    opt =>
    {
        opt.User.RequireUniqueEmail = true;
        opt.Password.RequireDigit = true;
        opt.Password.RequiredLength = 8;
        opt.Password.RequireLowercase = true;
        opt.Password.RequireNonAlphanumeric = true;
        opt.Password.RequireUppercase = true;
    })
    .AddEntityFrameworkStores<CircuitContext>()
    .AddClaimsPrincipalFactory<ApplicationClaimsFactory>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<CircuitContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Auto-circuit")));

builder.Services.AddScoped<BaseRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ContractRepository>();
builder.Services.AddScoped<VacationRepository>();
builder.Services.AddScoped(typeof(IRepository), typeof(BaseRepository));
builder.Services.AddTransient<EmailSenderService>();
builder.Services.AddScoped<ICurrentUser, CurrentUserService>();


builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();