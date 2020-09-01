using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Accounts;
using WebApi.Models.Projects;
using WebApi.Repositories;

namespace WebApi.Services
{
    public interface IProjectService
    {
        public Task<ProjectResponse> Create(CreateProjectRequest model, Account account);
        public Task<List<ProjectResponse>> Fetch(ReadProjectRequest projectRequest, Account account);
        public Task<ProjectResponse> Delete(int id, int accountId);
        public Task<ProjectResponse> Update(UpdateProjectRequest model, int accountId);
    }

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public ProjectService(
            IProjectRepository repository,
             IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProjectResponse> Create(CreateProjectRequest model, Account account)
        {
            if (model == null) ErrorMessages.Throw(ErrorMessages.Code.BadRequest);
            var entity = _mapper.Map<Project>(model);
            entity.AccountId = account.AccountId;
            var response = await _repository.AddAsync(entity);
            return _mapper.Map<ProjectResponse>(response);
        }

        public async Task<List<ProjectResponse>> Fetch(ReadProjectRequest request, Account account)
        {
            var entities = await _repository.FetchProjectsAsync(request, account);
            if (entities == null || entities.Count == 0)
                return null;
            List<ProjectResponse> responses = new List<ProjectResponse>();
            foreach (var entity in entities)
            {
                var response = _mapper.Map<ProjectResponse>(entity);
                if (request.FetchFullTree ?? false)
                {
                    var entityIndex = entities.IndexOf(entity);
                    _mapper.Map(entity.WorkItems, response.WorkItems);
                    _mapper.Map(entity.ProjectMembers, response.ProjectMembers);
                    foreach (var projectMember in entity.ProjectMembers)
                    {
                        var i = entity.ProjectMembers.IndexOf(projectMember);
                        response.ProjectMembers[i].ProjectName = entity.Name;
                        response.ProjectMembers[i].ShortAccount = _mapper.Map<ShortAccountResponse>(entity.ProjectMembers[i].Account);
                        response.ProjectMembers[i].Account = null;
                    }
                }
                responses.Add(response);
            }
            return responses;
        }

        public async Task<ProjectResponse> Update(UpdateProjectRequest model, int accountId)
        {
            if (model == null) ErrorMessages.Throw(ErrorMessages.Code.BadRequest);
            var entity = AuthorizedEntity(model.ProjectId, accountId);
            _mapper.Map(model, entity);
            entity.Modified = DateTime.UtcNow;
            var response = await _repository.UpdateAsync(entity);
            return _mapper.Map<ProjectResponse>(response);
        }

        public async Task<ProjectResponse> Delete(int id, int accountId)
        {
            AuthorizedEntity(id, accountId);
            var response = await _repository.DeleteAsync(id, accountId);
            return _mapper.Map<ProjectResponse>(response);
        }

        public Project AuthorizedEntity(int id, int accountId)
        {
            var entityToUpdate = _repository.FetchById(id);
            if (entityToUpdate == null)
                ErrorMessages.Throw(ErrorMessages.Code.BadRequest);

            if (entityToUpdate.AccountId != accountId)
                ErrorMessages.Throw(ErrorMessages.Code.UnAuthorized);
            return entityToUpdate;
        }
    }
}
