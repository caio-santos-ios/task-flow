using to_do_list.src.Infraestructure;
using to_do_list.src.Interfaces;
using to_do_list.src.Models;
using to_do_list.src.Models.Base;
using to_do_list.src.Shared.DTOs;
using to_do_list.src.Shared.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace to_do_list.src.Repository
{
    public class TaskRepository(AppDbContext context) : ITaskRepository
    {
        #region CREATE
        public async Task<ResponseApi<to_do_list.src.Models.Task?>> CreateAsync(to_do_list.src.Models.Task user)
        {
            try
            {
                await context.Tasks.InsertOneAsync(user);
                return new(user, 201, "Tarefa criado com sucesso");
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");   
            }
        }
        #endregion
        #region READ
        public async Task<ResponseApi<List<dynamic>>> GetAllAsync(PaginationUtil<to_do_list.src.Models.Task> pagination)
        {
            try
            {
                List<BsonDocument> pipeline = new()
                {
                    new("$match", pagination.PipelineFilter),
                    new("$sort", pagination.PipelineSort),
                    new("$skip", pagination.Skip),
                    new("$limit", pagination.Limit),
                    
                    new("$project", new BsonDocument
                    {
                        {"_id", 0},
                        {"id", new BsonDocument("$toString", "$_id")},
                        {"title", 1},
                        {"description", 1},
                        {"startDate", 1},
                        {"endDate", 1},
                        {"priority", 1},
                        {"status", 1},
                        {"categoryId", 1},
                    }),
                    new("$sort", pagination.PipelineSort),
                };

                List<BsonDocument> results = await context.Tasks.Aggregate<BsonDocument>(pipeline).ToListAsync();
                List<dynamic> list = results.Select(doc => BsonSerializer.Deserialize<dynamic>(doc)).ToList();
                return new(list);
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }
        public async Task<ResponseApi<dynamic?>> GetByIdAggregateAsync(string id)
        {
            try
            {
                BsonDocument[] pipeline = [
                    new("$match", new BsonDocument{
                        {"_id", new ObjectId(id)},
                        {"deleted", false}
                    }),

                    new("$addFields", new BsonDocument
                    {
                        {"id", new BsonDocument("$toString", "$_id")},
                    }),

                    new("$project", new BsonDocument
                    {
                        {"_id", 0},
                        {"id", new BsonDocument("$toString", "$_id")},
                        {"title", 1},
                        {"description", 1},
                        {"startDate", 1},
                        {"endDate", 1},
                        {"priority", 1},
                        {"status", 1},
                        {"categoryId", 1},
                    }),
                ];

                BsonDocument? response = await context.Tasks.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
                dynamic? result = response is null ? null : BsonSerializer.Deserialize<dynamic>(response);
                return result is null ? new(null, 404, "Tarefa não encontrado") : new(result);
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }
        public async Task<ResponseApi<to_do_list.src.Models.Task?>> GetByIdAsync(string id)
        {
            try
            {
                to_do_list.src.Models.Task? user = await context.Tasks.Find(x => x.Id == id && !x.Deleted).FirstOrDefaultAsync();
                return new(user);
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }
        public async Task<int> GetCountDocumentsAsync(PaginationUtil<to_do_list.src.Models.Task> pagination)
        {
            List<BsonDocument> pipeline = new()
            {
                new("$match", pagination.PipelineFilter),
                new("$sort", pagination.PipelineSort),
                new("$addFields", new BsonDocument
                {
                    {"id", new BsonDocument("$toString", "$_id")},
                }),
                new("$project", new BsonDocument
                {
                    {"_id", 0},
                    {"password", 0},
                    {"role", 0},
                    {"blocked", 0},
                    {"codeAccess", 0},
                    {"validatedAccess", 0}
                }),
                new("$sort", pagination.PipelineSort),
            };

            List<BsonDocument> results = await context.Tasks.Aggregate<BsonDocument>(pipeline).ToListAsync();
            return results.Select(doc => BsonSerializer.Deserialize<dynamic>(doc)).Count();
        }
        #endregion
        #region UPDATE
        public async Task<ResponseApi<to_do_list.src.Models.Task?>> UpdateAsync(to_do_list.src.Models.Task user)
        {
            try
            {
                await context.Tasks.ReplaceOneAsync(x => x.Id == user.Id, user);
                return new(user, 200, "Tarefa atualizado com sucesso");
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }
        #endregion
        #region DELETE
        public async Task<ResponseApi<to_do_list.src.Models.Task>> DeleteAsync(DeleteDTO request)
        {
            try
            {
                to_do_list.src.Models.Task? user = await context.Tasks.Find(x => x.Id == request.Id && !x.Deleted).FirstOrDefaultAsync();
                if(user is null) return new(null, 404, "Tarefa não encontrado");
                
                user.Deleted = true;
                user.DeletedAt = DateTime.UtcNow;
                user.DeletedBy = request.DeletedBy;

                await context.Tasks.ReplaceOneAsync(x => x.Id == user.Id, user);

                return new(user, 204, "Tarefa excluído com sucesso");
            }
            catch
            {
                return new(null, 500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }
        #endregion
    }
}