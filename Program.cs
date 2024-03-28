using System.Text.Json;
using Connectify.Infrastructure.Persistence.Repositories;
using Connectify.Infrastructure.Persistence.Services;
using Connectify.src.Application.Attributes;
using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.Repositories;
using Connectify.src.Application.Services;
using Connectify.src.Infrastructure.Mailgun;
using Connectify.src.Infrastructure.Persistence;
using Connectify.src.Infrastructure.Persistence.Repositories;
using Connectify.src.Infrastructure.Persistence.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")!));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

builder.Services.AddScoped<IValidator<SignAttemptRequest>, SignAttemptValidator>();
builder.Services.AddScoped<IValidator<LogAttemptRequest>, LogAttemptValidator>();
builder.Services.AddScoped<IValidator<SendConfirmationCodeRequest>, SendConfirmationCodeValidator>();
builder.Services.AddScoped<IValidator<VerifyConfirmationCodeRequest>, VerifyConfirmationCodeValidator>();
builder.Services.AddScoped<IValidator<CreatePostRequest>, CreatePostValidator>();
builder.Services.AddScoped<IValidator<UpdatePostRequest>, UpdatePostValidator>();
builder.Services.AddScoped<IValidator<CreateCommentRequest>, CreateCommentValidator>();
builder.Services.AddScoped<IValidator<UpdateCommentRequest>, UpdateCommentValidator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<IFriendshipRepository, FriendshipRepository>();
builder.Services.AddScoped<IAuthTokenRepository, AuthTokenRepository>();
builder.Services.AddScoped<IEmailConfirmationCodeRepository, EmailConfirmationCodeRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFriendShipService, FriendshipService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
builder.Services.AddScoped<ISendEmailService, SendEmailService>();

builder.Services.AddScoped(typeof(ValidateUser<>)); 

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = 134217728);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();