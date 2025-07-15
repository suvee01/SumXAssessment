using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using SumXAssessment.API.Services;
using SumXAssginment.Application;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Infrastructure;
using SumXAssignment.Infrastructure.Repository;
using System.Text; // Ensure this namespace is added  

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  
builder.Services.AddControllers();

builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddSumXAssignmentInfraServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Identity
builder.Services.AddIdentity<EUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Configure JWT Authentication
builder.Services.AddAuthenticationService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
