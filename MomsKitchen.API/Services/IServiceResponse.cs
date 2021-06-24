using System.Collections.Generic;

namespace MomsKitchen.API.Services
{
    public interface IServiceResponse<Entity> where Entity : class
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<Entity> Results { get; set; }

        public Entity Result { get; set; }

        ServiceResponse<Entity> Successfull();

        ServiceResponse<Entity> Successfull(Entity entity);

        ServiceResponse<Entity> Successfull(List<Entity> entities);

        ServiceResponse<Entity> Error();

        ServiceResponse<Entity> Error(string message);
    }
}