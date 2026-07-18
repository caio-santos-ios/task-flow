using to_do_list.src.Models;
using to_do_list.src.Models.Base;
using to_do_list.src.Shared.DTOs;

namespace to_do_list.src.Interfaces.INotification
{
    public interface INotificationService
    {
        public Task<ResponseApi<Notification?>> CreateAsync(Notification request);
        Task<ResponseApi<List<dynamic>>> GetAllAsync(GetAllDTO request);      
    }
}