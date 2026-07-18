

using to_do_list.src.Models;
using to_do_list.src.Models.Base;
using to_do_list.src.Requests;
using to_do_list.src.Shared.DTOs;

namespace to_do_list.src.Interfaces
{
    public interface IUserService
    {
        Task<ResponseApi<PaginationApi<List<dynamic>>>> GetAllAsync(GetAllDTO request);
        Task<ResponseApi<dynamic?>> GetByIdAggregateAsync(string id);
        Task<ResponseApi<List<User>>> GetNotByIdAsync(string id);
        Task<ResponseApi<User?>> CreateAsync(CreateUserDTO user);
        Task<ResponseApi<User?>> UpdateAsync(UpdateUserDTO user);
        Task<ResponseApi<User?>> UpdateConfirmAccountAsync(UpdateConfirmAccountDTO request);
        Task<ResponseApi<dynamic?>> UpdateFCMAsync(UpdateFCMUserDTO request);
        Task<ResponseApi<string>> ProfilePhotoAsync(ProfilePhotoDTO request);
        Task<ResponseApi<string>> RemoveProfilePhotoAsync(ProfilePhotoDTO request);
        Task<ResponseApi<User>> DeleteAsync(DeleteDTO request);
    }
}