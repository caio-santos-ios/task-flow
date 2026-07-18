using to_do_list.src.Models.Base;
using to_do_list.src.Requests;

namespace to_do_list.src.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseApi<dynamic?>> LoginAsync(LoginRequest request);
        Task<ResponseApi<dynamic?>> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<ResponseApi<dynamic?>> ResetPasswordAsync(ResetPasswordRequest request);
    }
}