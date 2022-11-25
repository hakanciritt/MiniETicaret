using ETicaret.API.MÝddlewares;
using ETicaret.Application;
using ETicaret.Application.Constants;
using ETicaret.Application.ViewModels.Products;
using ETicaret.Infrastructure;
using ETicaret.Infrastructure.Filters;
using ETicaret.Infrastructure.Services.Storage.Local;
using ETicaret.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using SignalR;
using System.Data;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();

Logger log = new LoggerConfiguration()
    //write to ile baþlayan her þeyde loglama yapacaðýmýz kaynaklarý belirtiriz
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
        "Logs",
        columnOptions: new ColumnOptions()
        {
            AdditionalColumns = new List<SqlColumn>()
            {
                new ("message",SqlDbType.VarChar),
                new ("message_template",SqlDbType.VarChar),
                new ("exception" , SqlDbType.VarChar),
                new ("log_event" , SqlDbType.VarChar),
                //new ("user_name" , SqlDbType.VarChar)
            }
        },
        autoCreateSqlTable: true)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddStorage<LocalStorage>();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4201", "https://localhost:4201")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()));

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();
        //options.Filters.Add<CustomAuthorize>();
    }).AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<CreateProductViewModel>());

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(Schemes.AdminScheme, options =>
    {
        //options.Events = new JwtBearerEvents()
        //{
        //    OnMessageReceived = context =>
        //    {
        //        context.Token = context.Request.Cookies[""];
        //        return Task.CompletedTask;
        //    }
        //};
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //oluþturulacak token deðerini hangi sitelerin kullanacaðýný belirlediðimiz deðerdir.
            ValidateIssuer = true, //oluþturulacak tokeni kimin daðýttýðýný belirttiðimiz kýsýmdýr
            ValidateLifetime = true, // token deðerinin süresini kontrol edecek doðrulamadýr.
            ValidateIssuerSigningKey = true, //üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden security key verisinin doðrulanmasýdýr
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires.HasValue ? expires.Value > DateTime.UtcNow : false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            NameClaimType = ClaimTypes.Name,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<object>>());
app.UseStaticFiles();
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    string username = context.User?.Identity?.Name != null || true ? context.User.Identity.Name : null;
    if (!string.IsNullOrEmpty(username))
        LogContext.PushProperty("user_name", username);

    await next();
});

 app.MapControllers();

app.MapHubs();

app.Run();
