using GraphQL.Context;
using GraphQL.Interfaces;
using GraphQL.Mutations;
using GraphQL.Quries;
using GraphQL.Repositories;
using GraphQL.Services;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Configure GraphQL with separated Query and Mutation types
builder.Services
    .AddGraphQLServer()
    .AddQueryType<GraphQL.Quries.Query>()
    .AddTypeExtension<UserQueries>()
    .AddTypeExtension<EmployeeQueries>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<UserMutations>()
    .AddTypeExtension<EmployeeMutations>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

// Add CORS (optional, for frontend integration)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure middleware
app.UseCors();

// GraphQL endpoint
app.MapGraphQL().WithOptions(new GraphQLServerOptions
{
    Tool = { Enable = true }
});

// Voyager UI
app.UseVoyager("/graphql", "/voyager");
app.Run();
