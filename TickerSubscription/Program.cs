using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TickerSubscription.DeribitApi.Clients;
using TickerSubscription.DeribitApi.Factories;
using TickerSubscription.DeribitApi.Models;
using TickerSubscription.DeribitApi.Services;
using TickerSubscription.DeribitApi.Utils;
using TickerSubscription.Extensions;
using TickerSubscription.Mappers;
using TickerSubscription.NotificationHandlers;
using TickerSubscription.Services;
using TickerSubscription.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfigurationSettings();
builder.Services.AddHostedService<SubscriptionWorker>();

builder.Services.AddSingleton<ISubscriptionWorkerService, SubscriptionWorkerService>();
builder.Services.AddSingleton<INotificationHandler, TickerNotificationHandler>();

builder.Services.AddSingleton<IRpcToJsonRpcConnectionHandler, RpcToJsonRpcConnectionHandler>();
builder.Services.AddSingleton<IConnectionCheckHandlerFactory, ConnectionCheckHandlerFactory>();

builder.Services.AddSingleton<IDeribitRpcClient, DeribitRpcClient>();
builder.Services.AddSingleton<ISubscriptionRequestMapper<Instrument>, InstrumentSubscriptionRequestMapper>();

builder.Services.AddSingleton<IDeribitService, DeribitService>();

var app = builder.Build();

app.Run();
