using MongoDB.Driver;
using to_do_list.src.Infraestructure;
using to_do_list.src.Interfaces;
using to_do_list.src.Models.Base;
using to_do_list.src.Shared.Utils;

namespace to_do_list.src.Repository
{
    public class DashboardRepository(AppDbContext context) : IDashboardRepository
    {
        public async Task<ResponseApi<dynamic>> GetAllAsync(PaginationUtil<dynamic> pagination)
        {
            try
            {
                DateTime date = DateTime.Now;

                long today = await context.Tasks.Find(x => !x.Deleted && x.StartDate.Date == date.Date).CountDocumentsAsync();
                long doing = await context.Tasks.Find(x => !x.Deleted && x.Status == "PENDENTE").CountDocumentsAsync();
                long late = await context.Tasks.Find(x => !x.Deleted && x.Status == "PENDENTE" && x.EndDate.Date < date.Date).CountDocumentsAsync();
                long finish = await context.Tasks.Find(x => !x.Deleted && x.Status == "FINALIZADO").CountDocumentsAsync();

                dynamic data = new
                {
                    today,
                    doing,
                    late,
                    finish
                };

                return new(data);
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");   
            }
        }
    }
}