var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSeq(serverUrl: "http://localhost:5341"));

builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);
builder.Services.AddControllers();
builder.Services.AddHostedService<SetupHostedService>();
builder.Services.AddDbContextPool<TicketDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration["Postgres:ConnectionString"], o => { o.EnableRetryOnFailure(); }));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();