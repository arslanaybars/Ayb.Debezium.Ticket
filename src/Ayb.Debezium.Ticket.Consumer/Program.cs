IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddConfiguration(new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build());
    })
    .ConfigureServices(services =>
    {
        services.AddLogging(loggingBuilder => loggingBuilder.AddSeq(serverUrl: "http://localhost:5341").AddConsole());

        services.AddScoped<ITicketMongoContext, TicketMongoContext>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        
        services.AddKafkaConsumer(opt =>
        {
            opt.AddEvent<string, TicketCreatedEvent, TicketCreatedEvent.Handler>()
                .AddEvent<string, TicketUpdatedEvent, TicketUpdatedEvent.Handler>()
                .AddEvent<string, TicketDeletedEvent, TicketDeletedEvent.Handler>();
        });
        services.AddHostedService<KafkaEventConsumer<string, TicketEventBase>>();

    })
    .Build();

await host.RunAsync();