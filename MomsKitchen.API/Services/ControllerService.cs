using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MomsKitchen.API.Constants;
using MomsKitchen.API.Repositories;
using MomsKitchen.API.Services.Auth;
using MomsKitchen.DATA.Exceptions;

namespace MomsKitchen.API.Services
{
    public class ControllerService<Entity, PostRequest, UpdateRequest> : IControllerService<Entity, PostRequest, UpdateRequest> 
        where Entity: class 
        where PostRequest: class 
        where UpdateRequest: class
    {
        protected readonly IRepository<Entity> _repository;

        protected readonly IAuthService _authService;

        protected readonly IMapper _mapper;

        public ControllerService(
            IRepository<Entity> repository,
            IMapper mapper,
            IAuthService authService
        )
        {
            _repository = repository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<Entity> Get(Guid entityId) => await _repository.Find(entityId);

        public async Task<ICollection<Entity>> GetAll() => await _repository.GetAll();

        public async Task<bool> Create(PostRequest request)
        {
            var entity = _mapper.Map<Entity>(request);

            entity = SetCreateProperties(entity);

            return await _repository.Add(entity);
        }

        public async Task<bool> Delete(Guid entityId)
        {
            var entity = await _repository.Find(entityId);

            if (entity == null) 
                throw new NotFoundException(ErrorMessages.NotFound);

            entity = SetDeleteProperties(entity);

            return await _repository.Update(entity);
        }

        public async Task<bool> Update(Guid entityId, UpdateRequest request)
        {
            var entity = await _repository.Find(entityId);

            entity = _mapper.Map(request, entity);

            entity = SetUpdateProperties(entity);

            return await _repository.Update(entity);
        }

        #region Timestamp properties

        public Entity SetCreateProperties(Entity entity)
        {
            entity
                .GetType()
                .GetProperty("CreatedAt")
                .SetValue(entity, DateTime.Now);

            entity
                .GetType()
                .GetProperty("CreatedBy")
                .SetValue(entity, _authService.GetLoggedUserId());

            entity
                .GetType()
                .GetProperty("UpdatedAt")
                .SetValue(entity, DateTime.Now);
            entity
                .GetType()
                .GetProperty("UpdatedBy")
                .SetValue(entity, _authService.GetLoggedUserId());

            entity.GetType().GetProperty("IsActive").SetValue(entity, true);
            entity
                .GetType()
                .GetProperty("ActivityUpdatedAt")
                .SetValue(entity, DateTime.Now);
            entity
                .GetType()
                .GetProperty("ActivityUpdatedBy")
                .SetValue(entity, _authService.GetLoggedUserId());

            return entity;
        }

        public Entity SetUpdateProperties(Entity entity)
        {
            entity
                .GetType()
                .GetProperty("UpdatedAt")
                .SetValue(entity, DateTime.Now);
            entity
                .GetType()
                .GetProperty("UpdatedBy")
                .SetValue(entity, _authService.GetLoggedUserId());

            entity.GetType().GetProperty("IsActive").SetValue(entity, true);
            entity
                .GetType()
                .GetProperty("ActivityUpdatedAt")
                .SetValue(entity, DateTime.Now);
            entity
                .GetType()
                .GetProperty("ActivityUpdatedBy")
                .SetValue(entity, _authService.GetLoggedUserId());

            return entity;
        }

        public Entity SetDeleteProperties(Entity entity)
        {
            entity
                .GetType()
                .GetProperty("IsDeleted")
                .SetValue(entity, true);
            entity
                .GetType()
                .GetProperty("DeletedAt")
                .SetValue(entity, DateTime.Now);
            entity
                .GetType()
                .GetProperty("DeletedBy")
                .SetValue(entity, _authService.GetLoggedUserId());

            return entity;
        }

        #endregion

    }
}
