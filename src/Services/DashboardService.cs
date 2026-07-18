using to_do_list.src.Interfaces;
using to_do_list.src.Models.Base;
using to_do_list.src.Shared.DTOs;
using to_do_list.src.Shared.Utils;

namespace to_do_list.src.Services
{
    public class DashboardService(
        IDashboardRepository repository
    ) : IDashboardService
    {
        public async Task<ResponseApi<dynamic>> GetAllAsync(GetAllDTO request)
        {
            try
            {
                PaginationUtil<dynamic> pagination = new(request.QueryParams);
                ResponseApi<dynamic> data = await repository.GetAllAsync(pagination);
                // dynamic data = new
                // {
                //     today = 0,
                //     doing = 0,
                //     late = 0,
                //     finish = 0
                // };

                return new(data.Data, 200, "Dashboard listados com sucesso");
            }
            catch (Exception ex)
            {
                return new(null, 500, $"Ocorreu um erro inesperado. Por favor, tente novamente mais tarde. {ex.Message}");
            }
        }
    }
}