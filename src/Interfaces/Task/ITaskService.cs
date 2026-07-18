

using to_do_list.src.Models.Base;
using to_do_list.src.Requests;
using to_do_list.src.Shared.DTOs;

namespace to_do_list.src.Interfaces
{
    public interface ITaskService
    {
        Task<ResponseApi<PaginationApi<List<dynamic>>>> GetAllAsync(GetAllDTO request);
        Task<ResponseApi<dynamic?>> GetByIdAggregateAsync(string id);
        Task<ResponseApi<to_do_list.src.Models.Task?>> CreateAsync(CreateTaskRequest request);
        Task<ResponseApi<to_do_list.src.Models.Task?>> UpdateAsync(UpdateTaskRequest request);
        Task<ResponseApi<to_do_list.src.Models.Task?>> FinishAsync(FinishTaskRequest request);
        Task<ResponseApi<to_do_list.src.Models.Task>> DeleteAsync(DeleteDTO request);
    }
}