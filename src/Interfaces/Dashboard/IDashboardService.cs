

using to_do_list.src.Models.Base;
using to_do_list.src.Shared.DTOs;

namespace to_do_list.src.Interfaces
{
    public interface IDashboardService
    {
        Task<ResponseApi<dynamic>> GetAllAsync(string userId);
    }
}