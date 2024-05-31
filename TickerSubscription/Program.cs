var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<SubscriptionWorker>();

var app = builder.Build();

app.Run();
