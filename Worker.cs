namespace HealthChecker;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            var res = HealthChecker.Check();
            foreach (var check in res.Where(check => check.IsOnline))
            {
                logger.LogInformation($"{check.SiteUrl} is online and healthy");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}