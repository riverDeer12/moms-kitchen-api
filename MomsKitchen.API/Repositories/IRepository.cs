using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MomsKitchen.API.Repositories
{
    public interface IRepository<Entity> where Entity: class
    {
        Task<List<Entity>> GetAll();

        Task<bool> Save();

        Task<bool> Add(Entity entity);

        Task<bool> Exists(Guid entityId);

        Task<Entity> Find(Guid entityId);

        Task<bool> Update(Entity entity);
    }
}