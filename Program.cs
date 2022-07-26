using DisneyApi.Data;
using DisneyApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Agregado visual de verificacion del token para verlo en Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese Bearer [token]"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>()
            }
        });
});


// Configuracion de la Base de Datos de Usuario utilizando EntityFramework

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserConnection>()
    .AddDefaultTokenProviders();

// Configuracion JWT 

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "https://localhost:7148",
        ValidIssuer = "https://localhost:7148",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyDeAlkemyParaElTokenJwtBearer"))
    };
});



// Configuracion para la conexion a Sql Server

builder.Services.AddDbContext<DisneyConnection>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DisneyConnection"));
}); 

builder.Services.AddDbContext<UserConnection>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection"));
});


var app = builder.Build();

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
