

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddConnectionString(builder.Configuration);
builder.Services.AddDependancies();
builder.Services.AddController();
builder.Services.AddSwagger();
builder.Services.AddFluentValidation();
builder.Services.AddMapsterConfig();


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
