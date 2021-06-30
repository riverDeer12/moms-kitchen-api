using System;
using System.Threading.Tasks;

namespace MomsKitchen.API.Services
{
    public interface IControllerService <Entity, PostRequest, UpdateRequest> 
        where Entity : class
        where PostRequest : class
        where UpdateRequest : class
    {
        Task<IServiceResponse<Entity>> GetAll();

        Task<IServiceResponse<Entity>> Get(Guid entityId);

        Task<IServiceResponse<Entity>> Delete(Guid entityId);

        Task<IServiceResponse<Entity>> Create(PostRequest request);

        Task<IServiceResponse<Entity>> Update(Guid entityId, UpdateRequest request);

        Entity SetCreateProperties(Entity entity);

        Entity SetUpdateProperties(Entity entity);

        Entity SetDeleteProperties(Entity entity);
    }
}
