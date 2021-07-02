using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MomsKitchen.API.Constants;
using MomsKitchen.API.Services.Auth;
using MomsKitchen.DATA;
using MomsKitchen.DATA.Exceptions;

namespace MomsKitchen.API.Repositories
{
    public class Repository<Entity>: IRepository<Entity> where Entity : class
    {
        private readonly Model _db;
        private readonly IAuthService _authService;

        public Repository(Model context, IAuthService authService)
        {
            _db = context;
            _authService = authService;
        }

        public async Task<bool> Add(Entity entity)
        {
            _db.Set<Entity>().Add(entity);

            return await Save();
        }

        public async Task<bool> Exists(Guid entityId)
        {
            return await Find(entityId) != null;
        }

        public async Task<Entity> Find(Guid entityId)
        {
            var entity = await _db.Set<Entity>().FindAsync(entityId);

            if(entity == null) 
                throw new NotFoundException(ErrorMessages.NotFound);

            return entity;
        }

        public async Task<List<Entity>> GetAll()
        {
            return await _db.Set<Entity>().ToListAsync();
        }

        public async Task<bool> Update(Entity entity)
        {
            _db.Set<Entity>().Update(entity);

            return await Save();
        }

        public async Task<bool> Save()
        {
            if (!_db.ChangeTracker.HasChanges()) return true;

            try
            {
                var result = await _db.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

            throw new BadRequestException(ErrorMessages.ErrorSaving);
        }
    }
}