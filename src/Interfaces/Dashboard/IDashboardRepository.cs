using to_do_list.src.Models.Base;
using to_do_list.src.Shared.Utils;

namespace to_do_list.src.Interfaces
{
    public interface IDashboardRepository
    {
        Task<ResponseApi<dynamic>> GetAllAsync(PaginationUtil<dynamic> pagination);
    }
}