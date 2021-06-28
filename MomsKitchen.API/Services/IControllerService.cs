using System.Threading.Tasks;

namespace MomsKitchen.API.Services
{
    public interface IControllerService <Entity, PostRequest, UpdateRequest> 
        where Entity : class
        where PostRequest : class
        where UpdateRequest : class
    {
        Task<IServiceResponse<Entity>> GetAll();

        Task<IServiceResponse<Entity>> Get(string entityId);

        Task<IServiceResponse<Entity>> Delete(string entityId);

        Task<IServiceResponse<Entity>> Create(PostRequest request);

        Task<IServiceResponse<Entity>> Update(string entityId, UpdateRequest request);

        Entity SetCreateProperties(Entity entity);

        Entity SetUpdateProperties(Entity entity);

        Entity SetDeleteProperties(Entity entity);
    }
}
