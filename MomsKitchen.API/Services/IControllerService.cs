using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MomsKitchen.API.Services
{
    public interface IControllerService <Entity, PostRequest, UpdateRequest> 
        where Entity : class
        where PostRequest : class
        where UpdateRequest : class
    {
        Task<List<Entity>> GetAll();

        Task<Entity> Get(Guid entityId);

        Task<bool> Delete(Guid entityId);

        Task<bool> Create(PostRequest request);

        Task<bool> Update(Guid entityId, UpdateRequest request);

        Entity SetCreateProperties(Entity entity);

        Entity SetUpdateProperties(Entity entity);

        Entity SetDeleteProperties(Entity entity);
    }
}
