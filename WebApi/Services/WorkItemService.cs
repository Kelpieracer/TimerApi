using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.WorkItems;
using WebApi.Repositories;

namespace WebApi.Services
{
    public interface IWorkItemService
    {
        public Task<WorkItemResponse> Create(CreateWorkItemRequest model, Account account);
        public WorkItemResponse Read(int id);
        public Task<WorkItemResponse> Delete(int id, int accountId);
        public Task<WorkItemResponse> Update(UpdateWorkItemRequest model, int accountId);
    }

    public class WorkItemService : IWorkItemService
    {
        private readonly IWorkItemRepository _repository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public WorkItemService(
            IWorkItemRepository repository,
             IMapper mapper,
             IProjectRepository projectRepository
            )
        {
            _repository = repository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<WorkItemResponse> Create(CreateWorkItemRequest model, Account account)
        {
            if (model == null) ErrorMessages.Throw(ErrorMessages.Code.BadRequest);
            var entity = _mapper.Map<WorkItem>(model);
            entity.AccountId = account.AccountId;
            var response = await _repository.AddAsync(entity);
            if (model.ProjectId != null)
            {
                var project =  _projectRepository.FetchById((int) model.ProjectId);
                project.WorkItems.Add(response);
            }
            return _mapper.Map<WorkItemResponse>(response);
        }

        public  WorkItemResponse Read(int id)
        {
            var entity = _repository.FetchById(id);
            if (entity == null)
                ErrorMessages.Throw(ErrorMessages.Code.NotFound);
            return _mapper.Map<WorkItemResponse>(entity);
        }

        public async Task<WorkItemResponse> Update(UpdateWorkItemRequest model, int accountId)
        {
            if (model == null) ErrorMessages.Throw(ErrorMessages.Code.BadRequest);
            var entity = AuthorizedEntity(model.WorkItemId, accountId);
            _mapper.Map(model, entity);
            entity.Modified = DateTime.UtcNow;
            var response = await _repository.UpdateAsync(entity);
            return _mapper.Map<WorkItemResponse>(response);
        }

        public async Task<WorkItemResponse> Delete(int id, int accountId)
        {
             AuthorizedEntity(id, accountId);
            var response = await _repository.DeleteAsync(id, accountId);
            return _mapper.Map<WorkItemResponse>(response);
        }

        public WorkItem AuthorizedEntity(int id, int accountId)
        {
            var entityToUpdate =  _repository.FetchById(id);
            if(entityToUpdate == null)
                ErrorMessages.Throw(ErrorMessages.Code.BadRequest);

            if (entityToUpdate.AccountId != accountId)
                ErrorMessages.Throw(ErrorMessages.Code.UnAuthorized);
            return entityToUpdate;
        }
    }
}
