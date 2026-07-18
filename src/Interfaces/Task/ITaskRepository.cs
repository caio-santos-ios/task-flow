using to_do_list.src.Models.Base;
using to_do_list.src.Shared.DTOs;
using to_do_list.src.Shared.Utils;

namespace to_do_list.src.Interfaces
{
    public interface ITaskRepository
    {
        Task<ResponseApi<List<dynamic>>> GetAllAsync(PaginationUtil<to_do_list.src.Models.Task> pagination);
        Task<ResponseApi<dynamic?>> GetByIdAggregateAsync(string id);
        Task<ResponseApi<to_do_list.src.Models.Task?>> GetByIdAsync(string id);
        Task<int> GetCountDocumentsAsync(PaginationUtil<to_do_list.src.Models.Task> pagination);
        Task<ResponseApi<to_do_list.src.Models.Task?>> CreateAsync(to_do_list.src.Models.Task user);
        Task<ResponseApi<to_do_list.src.Models.Task?>> UpdateAsync(to_do_list.src.Models.Task request);
        Task<ResponseApi<to_do_list.src.Models.Task>> DeleteAsync(DeleteDTO request);
    }
}