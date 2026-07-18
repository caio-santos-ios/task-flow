using to_do_list.src.Models;
using to_do_list.src.Models.Base;
using to_do_list.src.Shared.DTOs;
using to_do_list.src.Shared.Utils;

namespace to_do_list.src.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseApi<List<dynamic>>> GetAllAsync(PaginationUtil<User> pagination);
        Task<ResponseApi<dynamic?>> GetByIdAggregateAsync(string id);
        Task<ResponseApi<User?>> GetByIdAsync(string id);
        Task<ResponseApi<List<User>>> GetNotByIdAsync(string id);
        Task<ResponseApi<User?>> GetByEmailAsync(string email);
        Task<ResponseApi<User?>> GetByCodeAsync(string code);
        Task<int> GetCountDocumentsAsync(PaginationUtil<User> pagination);
        Task<ResponseApi<User?>> CreateAsync(User user);
        Task<ResponseApi<User?>> UpdateAsync(User request);
        Task<ResponseApi<User>> DeleteAsync(DeleteDTO request);
    }
}