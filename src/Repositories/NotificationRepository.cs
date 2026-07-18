using to_do_list.src.Infraestructure;
using to_do_list.src.Interfaces.INotification;
using to_do_list.src.Models;
using to_do_list.src.Models.Base;

namespace to_do_list.src.Repositories
{
    public class NotificationRepository(AppDbContext context) : INotificationRepository
    {
        public async Task<ResponseApi<Notification?>> CreateAsync(Notification notification)
        {
            try
            {
                await context.Notifications.InsertOneAsync(notification);
                return new(notification, 201, "Notificação criada com sucesso");
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }
    }
}