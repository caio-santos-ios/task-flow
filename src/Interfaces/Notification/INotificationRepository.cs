using to_do_list.src.Models;
using to_do_list.src.Models.Base;
using to_do_list.src.Shared.Utils;

namespace to_do_list.src.Interfaces.INotification
{
    public interface INotificationRepository
    {
        Task<ResponseApi<Notification?>> CreateAsync(Notification notification);
        Task<ResponseApi<List<dynamic>>> GetAllAsync(PaginationUtil<Notification> pagination);
    }
}