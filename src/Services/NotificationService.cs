using to_do_list.src.Interfaces.INotification;
using to_do_list.src.Models;
using to_do_list.src.Models.Base;
using to_do_list.src.Shared.DTOs;
using to_do_list.src.Shared.Utils;

namespace to_do_list.src.Services
{
    public class NotificationService(INotificationRepository repository) : INotificationService
    {
        public async Task<ResponseApi<Notification?>> CreateAsync(Notification request)
        {
            try
            {
                ResponseApi<Notification?> response = await repository.CreateAsync(request);

                if(response.Data is null) return new(null, 400, "Flha ao criar notificação");

                return new(response.Data, 201, "Notificação criada com sucesso");
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }
        public async Task<ResponseApi<List<dynamic>>> GetAllAsync(GetAllDTO request)
        {
            try
            {
                PaginationUtil<Notification> pagination = new(request.QueryParams);
                ResponseApi<List<dynamic>> notifications = await repository.GetAllAsync(pagination);
                return new(notifications.Data, 200, "Notificações listados com sucesso");
            }
            catch (Exception ex)
            {
                return new(null, 500, $"Ocorreu um erro inesperado. Por favor, tente novamente mais tarde. {ex.Message}");
            }
        }
    }
}