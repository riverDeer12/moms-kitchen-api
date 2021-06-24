using System.Collections.Generic;

namespace MomsKitchen.API.Services
{
    public class ServiceResponse<Entity> : IServiceResponse<Entity> where Entity : class
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<Entity> Results { get; set; }

        public Entity Result { get; set; }
        

        public ServiceResponse<Entity> Error() => new() { Success = false };

        public ServiceResponse<Entity> Successfull() => new() { Success = true };

        public ServiceResponse<Entity> Error(string message) => new() { Success = false, Message = message };

        public ServiceResponse<Entity> Successfull(Entity entity) => new() { Success = true, Result = entity };

        public ServiceResponse<Entity> Successfull(List<Entity> entities) => new() { Success = true, Results = entities };
    }
}