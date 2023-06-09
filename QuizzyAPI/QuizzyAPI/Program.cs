using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuizzyAPI.Data;
using QuizzyAPI.Identity;
using QuizzyAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS in development
if (builder.Environment.IsDevelopment()) {
    builder.Services.AddCors(options => {
        options.AddDefaultPolicy(policy => {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
}

// Add services to the container.
builder.Services.AddDbContext<QuizzyContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("QuizzyContextPG"));
});

// Setup Identity
builder.Services.AddIdentity<QuizzyUser, QuizzyRole>()
    .AddEntityFrameworkStores<QuizzyContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; 
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});
builder.Services.AddTransient<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Setup testing database
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<QuizzyContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    // Create an administrator account
    var userManager = services.GetRequiredService<UserManager<QuizzyUser>>();
    var password = builder.Configuration["AdminPassword"];

    if (password == null) {
        Console.WriteLine("AdminPassword is not set in configuration, not creating administrator account!");
    }

    await userManager.CreateAsync(new QuizzyUser() {
        UserName = "Admin",
        Email = "admin@example.com"
    }, password!);

    var user = await userManager.FindByNameAsync("Admin");

    await userManager.AddToRoleAsync(user, Constants.Roles.ADMINISTRATOR);
}

app.UseCors();
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();