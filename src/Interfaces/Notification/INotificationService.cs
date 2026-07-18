using to_do_list.src.Models;
using to_do_list.src.Models.Base;

namespace to_do_list.src.Interfaces.INotification
{
    public interface INotificationService
    {
        public Task<ResponseApi<Notification?>> CreateAsync(Notification request);       
    }
}