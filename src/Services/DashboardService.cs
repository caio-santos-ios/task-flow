using to_do_list.src.Interfaces;
using to_do_list.src.Models.Base;

namespace to_do_list.src.Services
{
    public class DashboardService(
        IDashboardRepository repository
    ) : IDashboardService
    {
        public async Task<ResponseApi<dynamic>> GetAllAsync(string userId)
        {
            try
            {
                ResponseApi<dynamic> data = await repository.GetAllAsync(userId);

                return new(data.Data, 200, "Dashboard listados com sucesso");
            }
            catch (Exception ex)
            {
                return new(null, 500, $"Ocorreu um erro inesperado. Por favor, tente novamente mais tarde. {ex.Message}");
            }
        }
    }
}