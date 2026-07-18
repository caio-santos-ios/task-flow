using to_do_list.src.Models.Base;
using to_do_list.src.Requests;
using to_do_list.src.Shared.DTOs;

namespace to_do_list.src.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseApi<PaginationApi<List<dynamic>>>> GetAllAsync(GetAllDTO request);
        Task<ResponseApi<List<dynamic>>> GetSelectAsync(GetAllDTO request);
        Task<ResponseApi<dynamic?>> GetByIdAggregateAsync(string id);
        Task<ResponseApi<to_do_list.src.Models.Category?>> CreateAsync(CreateCategoryRequest user);
        Task<ResponseApi<to_do_list.src.Models.Category?>> UpdateAsync(UpdateCategoryRequest user);
        Task<ResponseApi<to_do_list.src.Models.Category>> DeleteAsync(DeleteDTO request);
    }
}