using System.Net.Mime;
using System.Text;

namespace Ayb.Debezium.Ticket.Api;

public class SetupHostedService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SetupHostedService> _logger;

    public SetupHostedService(IServiceScopeFactory scopeFactory, ILogger<SetupHostedService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await InitializeDatabaseAsync();

        await InitializeDebeziumAsync();
        
        async Task InitializeDatabaseAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope
                .ServiceProvider
                .GetRequiredService<TicketDbContext>();

            await db
                .Database
                .EnsureCreatedAsync(cancellationToken);
        }
        
        async Task InitializeDebeziumAsync()
        {
            using var httpClient = new HttpClient();
            var str = await File.ReadAllTextAsync("debezium-outbox-config.json", cancellationToken);

            var response = await httpClient.PutAsync(
                "http://localhost:8083/connectors/outbox-connector/config",
                new StringContent(str,
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json), cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(
                    "Debezium outbox configured. Status code: {StatusCode} Response: {Response}",
                    response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
            }
            else
            {
                _logger.LogError("Failed to configure Debezium outbox. Status code: {StatusCode}, Response: {Response}",
                    response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
                throw new Exception("Failed to configure Debezium outbox.");
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}