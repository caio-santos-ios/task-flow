using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using to_do_list.src.Infraestructure;
using to_do_list.src.Interfaces.INotification;
using to_do_list.src.Models;
using to_do_list.src.Models.Base;
using to_do_list.src.Shared.Utils;

namespace to_do_list.src.Repositories
{
    public class NotificationRepository(AppDbContext context) : INotificationRepository
    {
        public async Task<ResponseApi<Notification?>> CreateAsync(Notification notification)
        {
            try
            {
                await context.Notifications.InsertOneAsync(notification);
                return new(notification, 201, "Notificação criada com sucesso");
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }
        public async Task<ResponseApi<List<dynamic>>> GetAllAsync(PaginationUtil<Notification> pagination)
        {
            try
            {
                List<BsonDocument> pipeline = new()
                {
                    new("$match", pagination.PipelineFilter),
                    new("$sort", pagination.PipelineSort),
                    
                    new("$project", new BsonDocument
                    {
                        {"_id", 0},
                        {"id", new BsonDocument("$toString", "$_id")},
                        {"title", 1},
                        {"description", 1},
                        {"read", 1},
                        {"send", 1},
                        {"sendBy", 1},
                        {"sendAt", 1},
                        {"link", 1},
                        {"icon", 1}
                    }),
                    new("$sort", pagination.PipelineSort),
                };

                List<BsonDocument> results = await context.Notifications.Aggregate<BsonDocument>(pipeline).ToListAsync();
                List<dynamic> list = results.Select(doc => BsonSerializer.Deserialize<dynamic>(doc)).ToList();
                return new(list);
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }
    }
}