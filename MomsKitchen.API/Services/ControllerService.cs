using System;
using System.Threading.Tasks;
using AutoMapper;
using MomsKitchen.API.Constants;
using MomsKitchen.API.Repositories;
using MomsKitchen.API.Services.Auth;

namespace MomsKitchen.API.Services
{
    public class ControllerService<Entity, PostRequest, UpdateRequest> : IControllerService<Entity, PostRequest, UpdateRequest> 
        where Entity: class 
        where PostRequest: class 
        where UpdateRequest: class
    {
        private readonly IRepository<Entity> _repository;

        private readonly IServiceResponse<Entity> _response;

        private readonly IAuthService _authService;

        private readonly IMapper _mapper;

        public ControllerService(
            IRepository<Entity> repository,
            IServiceResponse<Entity> response,
            IMapper mapper,
            IAuthService authService
        )
        {
            _repository = repository;
            _response = response;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<IServiceResponse<Entity>> Create(PostRequest request)
        {
            var entity = _mapper.Map<Entity>(request);

            entity = SetCreateProperties(entity);

            var result = await _repository.Add(entity);

            if (result) return _response.Successfull();

            return _response.Error(ErrorMessages.ErrorSaving);
        }

        public async Task<IServiceResponse<Entity>> Delete(Guid entityId)
        {
            var entity = await _repository.Find(entityId);

            if (entity == null) return _response.Error(ErrorMessages.NotFound);

            entity = SetDeleteProperties(entity);

            var result = await _repository.Update(entity);

            if (result) return _response.Successfull();

            return _response.Error(ErrorMessages.ErrorDeleting);
        }

        public async Task<IServiceResponse<Entity>> Get(Guid entityId)
        {
            var entity = await _repository.Find(entityId);

            if (entity == null) return _response.Error(ErrorMessages.NotFound);

            return _response.Successfull(entity);
        }

        public async Task<IServiceResponse<Entity>> GetAll()
        {
            var entities = await _repository.GetAll();

            return _response.Successfull(entities);
        }

        public async Task<IServiceResponse<Entity>> Update(Guid entityId, UpdateRequest request)
        {
            var entity = await _repository.Find(entityId);

            if (entity == null) return _response.Error(ErrorMessages.NotFound);

            entity = _mapper.Map(request, entity);

            entity = SetUpdateProperties(entity);

            var result = await _repository.Update(entity);

            if (result) return _response.Successfull();

            return _response.Error(ErrorMessages.ErrorUpdating);
        }

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
    }
}
