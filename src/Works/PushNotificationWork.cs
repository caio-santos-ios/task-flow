using FirebaseAdmin.Messaging;
using MongoDB.Driver;
using to_do_list.src.Infraestructure;

namespace to_do_list.src.Works
{
    public class PushNotificationWork(IServiceProvider serviceProvider, ILogger<PushNotificationWork> _logger) : BackgroundService
    {
        private static readonly TimeSpan CheckInterval = TimeSpan.FromSeconds(60);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessDueTriggers(stoppingToken);
                    _logger.LogInformation("Background task running at: {time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while executing background task.");
                }

                await Task.Delay(CheckInterval, stoppingToken);
            }
        }

        private async Task ProcessDueTriggers(CancellationToken ct)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await SendNotificationAsync(context);
        }

        private async Task SendNotificationAsync(AppDbContext context)
        {
            List<Models.Notification> notifications = await context.Notifications
                .Find(x => !x.Deleted && !x.Send)
                .ToListAsync();

            foreach (Models.Notification notification in notifications)
            {
                try
                {
                    List<string> tokens = await context.Users
                        .Find(x => x.Id == notification.SendBy)
                        .Project(x => x.TokenFCM)
                        .ToListAsync();

                    if (tokens.Count == 0)
                    {
                        _logger.LogWarning(
                            "Notificação {Id}: usuário {UserId} não tem tokens FCM registrados.",
                            notification.Id, notification.SendBy);

                        await MarkAsSentAsync(context, notification.Id);
                        continue;
                    }

                    var message = new MulticastMessage
                    {
                        Tokens = tokens,
                        Notification = new FirebaseAdmin.Messaging.Notification
                        {
                            Title = notification.Title,
                            Body = notification.Description,
                        },
                    };

                    var response = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message);

                    if (response.FailureCount > 0)
                    {
                        for (int i = 0; i < response.Responses.Count; i++)
                        {
                            if (!response.Responses[i].IsSuccess)
                            {
                                _logger.LogWarning(
                                    "Falha ao enviar pro token {Token}: {Error}",
                                    tokens[i], response.Responses[i].Exception?.Message);
                            }
                        }
                    }

                    await MarkAsSentAsync(context, notification.Id);

                    _logger.LogInformation(
                        "Notificação {Id} enviada: {Success} sucesso(s), {Failure} falha(s).",
                        notification.Id, response.SuccessCount, response.FailureCount);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao enviar notificação {Id}.", notification.Id);
                }
            }
        }

        private async Task MarkAsSentAsync(AppDbContext context, string notificationId)
        {
            var update = Builders<Models.Notification>.Update.Set(x => x.Send, true).Set(x => x.SendAt, DateTime.Now);
            await context.Notifications.UpdateOneAsync(x => x.Id == notificationId, update);
        }
    }
}