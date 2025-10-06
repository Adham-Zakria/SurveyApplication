using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Repositories;
using Services;
using Services.MappingProfiles;
using ServicesAbstraction;

var builder = WebApplication.CreateBuilder(args);


#region Add services to the container

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SurveyAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Repositories
builder.Services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IQuestionnaireService, QuestionnaireService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IBranchService, BranchService>();

// Access HttpContext for IP detection
builder.Services.AddHttpContextAccessor();

//Mapping profile
builder.Services.AddAutoMapper(p => p.AddProfile(new AnswerProfile()));
builder.Services.AddAutoMapper(p => p.AddProfile(new AuthProfile()));
builder.Services.AddAutoMapper(p => p.AddProfile(new BranchProfile()));
builder.Services.AddAutoMapper(p => p.AddProfile(new DepartmentProfile()));
builder.Services.AddAutoMapper(p => p.AddProfile(new QuestionnaireProfile()));
builder.Services.AddAutoMapper(p => p.AddProfile(new QuestionProfile()));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalNetwork",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin(); 
        });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

// Execute CORS
app.UseCors("AllowLocalNetwork");

app.MapControllers();

app.Run();
